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
        public int ID_user { get; set; }
        public int ID_flight { get; set; }
        public string Email { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postcode { get; set; }
        public string Class { get; set; }
        public int Price { get; set; }
        public string Seat { get; set; }
    }
}