using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DDACAssignment.Models
{
    public class Airport
    {
        [Key, StringLength(3)]
        public string Code { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
    }

    public class Flight
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightId { get; set; }

        public Airport Origin { get; set; }

        public Airport Destination { get; set; }

        public String AircraftId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mmtt}", ApplyFormatInEditMode = true)]
        public DateTime Departure { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mmtt}", ApplyFormatInEditMode = true)]
        public DateTime Arrival { get; set; }

        public double Price { get; set; }
    }

    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        public Flight Flight { get; set; }

        public List<Seat> Seats { get; set; }
    }

    public class Seat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}