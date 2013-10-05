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
        //[DataType(DataType.Date)]
        //public DateTime Departure_date { get; set; }
        //[DataType(DataType.Time)]
        //public DateTime Departure_time { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime Arrival_date { get; set; }
        //[DataType(DataType.Time)]
        //public DateTime Arrival_time { get; set; }
        public string Departure_date { get; set; }
        public string Departure_time { get; set; }
        public string Arrival_date { get; set; }
        public string Arrival_time { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Distance { get; set; }
    }

    public class SearchModel
    {
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string TripType { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime Departure_date1;
        //[DataType(DataType.Date)]
        //public DateTime Departure_date2;
        [Required]
        public string Departure_date1 { get; set; }
        public string Departure_date2 { get; set; }
        [Required]
        public int Sum { get; set; }
    }

    public class OptionsModel
    {
        public string TripType { get; set; }
        public List<Flight> Flights1 { get; set; }
        public List<Flight> Flights2 { get; set; }
        public int Sum { get; set; }
    }
}