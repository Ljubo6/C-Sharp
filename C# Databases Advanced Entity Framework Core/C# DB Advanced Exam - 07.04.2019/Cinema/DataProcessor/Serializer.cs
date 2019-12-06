namespace Cinema.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
                 .Where(m => m.Rating >= rating && m.Projections.Any(p => p.Tickets.Count > 0))
                 .OrderByDescending(m => m.Rating)
                 .ThenByDescending(m => m.Projections.SelectMany(p => p.Tickets)
                 .Sum(t => t.Price))
                 .Select(x => new
                 {
                     MovieName = x.Title,
                     Rating = x.Rating.ToString("F2"),
                     TotalIncomes = x.Projections.Select(p => p.Tickets.Sum(t => t.Price)).Sum().ToString("F2"),
                     Customers = x.Projections
                     .SelectMany(p => p.Tickets)
                     .Select(t => t.Customer)
                     .Select(c => new
                     {
                         FirstName = c.FirstName,
                         LastName = c.LastName,
                         Balance = c.Balance.ToString("F2")
                     })
                     .OrderByDescending(c => c.Balance)
                     .ThenBy(c => c.FirstName)
                     .ThenBy(c => c.LastName)
                     .ToArray()
                 })
                 .Take(10)
                 .ToArray();

            var json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(c => c.Tickets.Sum(t => t.Price))
                .Select(x => new ExportTopCustomerXmlDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SpentMoney = x.Tickets.Sum(t => t.Price).ToString("F2"),
                    SpentTime = TimeSpan.FromSeconds(x.Tickets.Select(t => t.Projection).Sum(p => p.Movie.Duration.TotalSeconds)).ToString(@"hh\:mm\:ss",CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportTopCustomerXmlDto[]), new XmlRootAttribute("Customers"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}