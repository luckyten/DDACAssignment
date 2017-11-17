using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDACAssignment.Models
{
    public class SearchViewModel
    {
        public List<Airport> Airports;
        public List<Flight> Flights;
    }

    public class BookingViewModel
    {
        public Flight Flight;
        public String BoookedSeats;
    }
}