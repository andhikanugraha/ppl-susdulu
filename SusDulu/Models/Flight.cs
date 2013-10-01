using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class Flight
    {
        public int ID { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departure { get; set; }
        public string level { get; set; }
    }

    public class FlightDBContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
    }
}