using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTrip(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description)
        {
            var trip = new Trip
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                DepartureTime = DateTime.ParseExact(departureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                ImagePath = imagePath,
                Seats = seats,
                Description = description,
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var usersTrips = new UserTrip
            {
                UserId = userId,
                TripId = tripId
            };

            if (this.db.UsersTrips.FirstOrDefault(x => x.UserId == userId && x.TripId == tripId) != null)
            {
                return false;
            }

            var trip = this.db.Trips.FirstOrDefault(x => x.Id == tripId);

            trip.Seats--;

            if (trip.Seats < 0)
            {
                return false;
            }

            this.db.UsersTrips.Add(usersTrips);
            this.db.SaveChanges();
            return true;
        }

        public IEnumerable<Trip> GetAll()
        {
            var trips = this.db.Trips.Select(x => new Trip
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                Seats = x.Seats

                
            })
                .Where(x => x.Seats > 0)
                .ToArray();
            return trips;
        }

        public Trip GetDetails(string tripId)
        {
            return this.db.Trips.FirstOrDefault(x => x.Id == tripId);

        }
    }
}
