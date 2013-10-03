using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public string Id_user { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        [NotMapped] [Compare("password", ErrorMessage = "Password doesn't match")]
        public String confirm_password { get; set; }
        public int distance_traveled { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}