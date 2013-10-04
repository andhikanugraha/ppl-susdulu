using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("flight")]
    public class Flight
    {
        [Key]
        public int ID { get; set; }
        public int ID_plane { get; set; }
        public string Flight_number { get; set; }
        [DataType(DataType.Date)]
        public DateTime Departure_date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Departure_time { get; set; }
        [DataType(DataType.Date)]
        public DateTime Arrival_date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Arrival_time { get; set; }
        public string Origin;
        public string Destination;
        public int Distance;
    }
}