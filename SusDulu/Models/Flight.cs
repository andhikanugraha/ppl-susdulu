using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class Flight
    {
        public int ID { get; set; }

        [Required]
        public string origin { get; set; }

        [Required]
        public string destination { get; set; }
        public Int32 distance { get; set; }

        [DataType(DataType.Date)]
        public DateTime schedule { get; set; }

        public string level { get; set; }
        public Int64 price { get; set; }
    }

    public class FlightDBContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
    }
}