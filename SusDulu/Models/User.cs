using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped, Compare("password", ErrorMessage = "Password doesn't match")]
        public string Confirm_password { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Total_miles { get; set; }
        public int Current_miles { get; set; }
    }
}