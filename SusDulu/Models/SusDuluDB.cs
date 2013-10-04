using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class SusDuluDB : DbContext
    {
        public SusDuluDB()
            : base("DefaultConnection")
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Places { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}