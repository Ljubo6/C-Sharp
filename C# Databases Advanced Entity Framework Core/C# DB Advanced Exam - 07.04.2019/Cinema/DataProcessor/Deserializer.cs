namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var moviesDto = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var movies = new List<Movie>();

            foreach (var movieDto in moviesDto)
            {
                var isTitleExist = movies.Any(x => x.Title == movieDto.Title);
                if (!IsValid(movieDto) || isTitleExist)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Movie movie = new Movie
                {
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    Duration = movieDto.Duration,
                    Rating = movieDto.Rating,
                    Director = movieDto.Director
                };

                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie,movie.Title,movie.Genre,movie.Rating.ToString("F2")));
            }
            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallsDto = JsonConvert.DeserializeObject<ImportHallAndSeatDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var halls = new List<Hall>();

            foreach (var hallDto in hallsDto)
            {
                if (!IsValid(hallDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Hall hall = new Hall
                {
                    Name = hallDto.Name,
                    Is4Dx = hallDto.Is4Dx,
                    Is3D = hallDto.Is3D
                };

                for (int i = 0; i < hallDto.Seats; i++)
                {
                    hall.Seats.Add(new Seat { Hall = hall });
                }

                halls.Add(hall);
                var hallProjectionType = GetProjectionType(hall.Is4Dx,hall.Is3D);
                sb.AppendLine(string.Format(SuccessfulImportHallSeat,hall.Name,hallProjectionType,hall.Seats.Count));
            }
            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static string GetProjectionType(bool is4Dx, bool is3D)
        {
            string output = "Normal";
            if (is4Dx && is3D)
            {
                output = "4Dx/3D";
            }
            else if (is4Dx && !is3D)
            {
                output = "4Dx";
            }
            else if(!is4Dx && is3D)
            {
                output = "3D";
            }
            return output;
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectionDto[]), new XmlRootAttribute("Projections"));
            var projectionsDto = (ImportProjectionDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var projections = new List<Projection>();

            foreach (var projectionDto in projectionsDto)
            {
                var isMovieExist = context.Movies.Any(x => x.Id == projectionDto.MovieId);
                var isHallExist = context.Halls.Any(x => x.Id == projectionDto.HallId);

                if ( !isMovieExist || !isHallExist)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    MovieId = projectionDto.MovieId,
                    HallId = projectionDto.HallId,
                    DateTime = DateTime
                    .ParseExact(projectionDto.DateTime, @"yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                projections.Add(projection);

                var projectionTitle = context.Movies.Find(projectionDto.MovieId).Title;
                sb.AppendLine(string.Format(SuccessfulImportProjection, projectionTitle, projection.DateTime.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture)));
            }
            context.Projections.AddRange(projections);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }


        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCustomerTicketDto[]), new XmlRootAttribute("Customers"));
            var customersDto = (ImportCustomerTicketDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            var customers = new List<Customer>();

            foreach (var customerDto in customersDto)
            {
                if (!IsValid(customerDto) || !customerDto.Tickets.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance,
                    Tickets = customerDto.Tickets
                    .Select(t => new Ticket
                    {
                        ProjectionId = t.ProjectionId,
                        Price = t.Price
                    })
                    .ToArray()
                    
                };

                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket,customer.FirstName,customer.LastName,customer.Tickets.Count));
            }
            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();
            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}