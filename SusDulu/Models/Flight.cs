using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("flight")]
    public class Flight
    {
        //public int ID { get; set; }
        [Key] 
        public string Id_flight { get; set; }
        public string schedule { get; set; }
        public int distance { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        //public DateTime departure { get; set; }
        //public string level { get; set; }
    }

    public class FlightDBContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
    }
}