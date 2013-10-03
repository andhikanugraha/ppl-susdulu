using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("user")]
        public int ID_user { get; set; }
        [ForeignKey("flight")]
        public int ID_flight { get; set; }
        public string Class { get; set; }
        public int Price { get; set; }
        public string Seat { get; set; }
    }
}