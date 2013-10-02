using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class SusDuluDbContext : DbContext
    {
        public DbSet<Flight> flight { get; set; }
        public DbSet<Ticket> ticket { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Plane> plane { get; set; }
        public DbSet<Use> use { get; set; }
        public DbSet<For> For { get; set; }
        public DbSet<Book> book { get; set; }
    }
}