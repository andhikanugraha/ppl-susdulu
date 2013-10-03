namespace SusDulu.Migrations
{
    using SusDulu.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SusDulu.Models.FlightDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SusDulu.Models.FlightDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Flights.AddOrUpdate(i => i.origin,
               new Flight
               {
                   origin = "JKT",
                   destination = "MDN",
                   distance = 200,
                   schedule = DateTime.Parse("2013-11-12 9:00:00 AM"),
                   level = "EXE",
                   price = 1000000

               },

               new Flight
               {
                   origin = "MDN",
                   destination = "JKT",
                   distance = 200,
                   schedule = DateTime.Parse("2013-12-13 12:00:00 AM"),
                   level = "UBS",
                   price = 1200000
               },

               new Flight
               {
                   origin = "BDG",
                   destination = "SBY",
                   distance = 500,
                   schedule = DateTime.Parse("2013-11-13 7:00:00 PM"),
                   level = "ECO",
                   price = 4000000
               }
               );
        }
    }
}
