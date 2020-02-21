using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        IEnumerable<Trip> GetAll();
        void AddTrip(string startingPoint, string endPoint, string departureTime, string carImage, int seats, string description);
        Trip GetDetails(string tripId);
        bool AddUserToTrip(string user, string tripId);
    }
}
