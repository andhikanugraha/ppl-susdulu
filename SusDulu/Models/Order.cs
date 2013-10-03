using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class Order
    {
        public int ID { get; set; } //orderID
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class OrderDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}