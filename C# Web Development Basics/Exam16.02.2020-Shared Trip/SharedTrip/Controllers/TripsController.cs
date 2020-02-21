using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.View();
            }
            if (input.Description.Length > 80)
            {
                return this.View();
            }
            this.tripsService.AddTrip(input.StartPoint,input.EndPoint,input.DepartureTime,input.ImagePath,input.Seats,input.Description);

            return this.Redirect("All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var trip = this.tripsService.GetDetails(tripId);
            return this.View(trip,"Details");
        }
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var userTrip = this.tripsService.AddUserToTrip(this.User, tripId);
            if (userTrip == false)
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }
            return this.Redirect("All");
        }
    }
}
