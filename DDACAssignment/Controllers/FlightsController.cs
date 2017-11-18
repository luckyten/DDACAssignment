using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DDACAssignment.Models;
using System.Globalization;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity;

namespace DDACAssignment.Controllers
{
    public class FlightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flights
        [HttpGet]
        public ActionResult Search()
        {
            SearchViewModel model = new SearchViewModel();
            List<Flight> flights = new List<Flight>();
            model.Flights = flights;
            model.Airports = db.Airports.OrderBy(x => x.City).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(string origin, string destination, string departureDate)
        {
            SearchViewModel model = new SearchViewModel();

            DateTime departure = DateTime.ParseExact(departureDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<Flight> flights = db.Flights.Include("Origin").Include("Destination").Where(x => x.Origin.Code == origin && x.Destination.Code == destination && DbFunctions.DiffDays(x.Departure, departure) == 0).ToList();
            model.Flights = flights;
            model.Airports = db.Airports.OrderBy(x => x.City).ToList();

            return PartialView("_Search", model);
        }

        [Authorize]
        // GET: Flights/Details/5
        public ActionResult Details(int id)
        {
            BookingViewModel model = new BookingViewModel();
            Flight flight = db.Flights.Include("Origin").Include("Destination").Where(x => x.FlightId == id).First();

            model.Flight = flight;

            List<Booking> bookings = db.Bookings.Include("Seats").Where(x => x.Flight.FlightId == id).ToList();
            string bookedSeats = "";

            foreach(var booking in bookings)
            {
                foreach(var seat in booking.Seats)
                {
                    bookedSeats += "'" + seat.Row + "_" + seat.Col + "',";
                }
            }
            bookedSeats = bookedSeats.TrimEnd(',');

            model.BoookedSeats = bookedSeats;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Book(int flightId, string seatList)
        {
            Booking b = new Booking();

            string userId = User.Identity.GetUserId();

            Flight flight = db.Flights.Find(flightId);
            b.Flight = flight;
            b.Seats = new List<Seat>();

            seatList = seatList.Trim(',');
            string[] seats = seatList.Split(',');

            foreach(var seat in seats)
            {
                string[] seatValue = seat.Split('_');

                Seat s = new Seat();
                s.Row = Convert.ToInt32(seatValue[0]);
                s.Col = Convert.ToInt32(seatValue[1]);

                b.Seats.Add(s);
            }

            db.Users.Find(userId).Bookings.Add(b);
            db.SaveChanges();

            return new JavaScriptResult { Script = "alert('Successfully Booked');" };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
