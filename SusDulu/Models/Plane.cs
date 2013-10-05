using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("plane")]
    public class Plane
    {
        [Key]
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Baggage_capacity { get; set; }
        public int Passenger_capacity { get; set; }
    }
}