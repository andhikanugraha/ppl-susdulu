using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("plane")]
    public class Plane
    {
        [Key] 
        public string Id_plane { get; set; }
        public string model { get; set; }
        public int baggage_capacity { get; set; }
        public int passenger_capacity { get; set; }
    }

    public class PlaneDBContext : DbContext
    {
        public DbSet<Plane> Planes { get; set; }
    }
}