using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("penerbangan")]
    public class Flight
    {
        [Key, Column("kodepenerbangan")]
        public int ID { get; set; }
        [Column("kodepesawat")]
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
        [Column("tanggalpenerbangan")]
        public string Departure_date { get; set; }
        [Column("waktupenerbangan")]
        public string Departure_time { get; set; }
        public string Arrival_date { get; set; }
        public string Arrival_time { get; set; }
        [Column("kotaasalbandara")]
        public string Origin { get; set; }
        [Column("kotatujuanbandara")]
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