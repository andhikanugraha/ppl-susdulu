using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SusDulu.Helpers;

namespace SusDulu.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // hashed using PBKDF2
        [NotMapped, Compare("Password", ErrorMessage = "Password doesn't match")]
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

        public User(string email, string unhashedPassword)
        {
            Email = email;
            SetUnhashedPassword(unhashedPassword);
        }

        public void SetUnhashedPassword(string newUnhashedPassword)
        {
            Password = PasswordHasher.CreateHash(newUnhashedPassword);
        }
    }

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        public User GetUser(string email)
        {
            return Users.Where<User>(u => u.Email == email).FirstOrDefault();
        }

        public User GetUser(int id)
        {
            return Users.Find(id);
        }

        public User GetUser(object obj)
        {
            return Users.Find(Convert.ToInt32(obj));
        }

        public User GetUser(string email, string unhashedPassword)
        {
            User user = GetUser(email);

            if (user != null)
            {
                // User found. Retrieve password hash.
                string correctHashedPassword = user.Password;
                bool valid = PasswordHasher.ValidatePassword(unhashedPassword, correctHashedPassword);

                return user;
            }
            else
            {
                return null;
            }
        }

        public bool ValidateUser(string email, string unhashedPassword)
        {
            return (GetUser(email, unhashedPassword) != null);
        }
    }
}