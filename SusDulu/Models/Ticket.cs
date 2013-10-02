using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Key] 
        public string Id_ticket { get; set; }
        public string Class { get; set; }
        public int price { get; set; }
        public string seat { get; set; }
    }

    public class TicketDBContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }
}