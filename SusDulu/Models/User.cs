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

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } // hashed using PBKDF2

        [Required]
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

        public User()
        {

        }

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


    public class LoginModel
    {
        [Required]
        [Display(Name = "Alamat surel")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sandilewat")]
        public string Password { get; set; }

        [Display(Name = "Ingat saya?")]
        public bool RememberMe { get; set; }
    }

    [NotMapped]
    public class RegisterModel : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public User ConvertToUser()
        {
            var u = new User();
            u.Email = Email;

            u.First_name = First_name;
            u.Middle_name = Middle_name;
            u.Last_name = Last_name;
            u.Gender = Gender;
            
            u.Address = Address;
            u.City = City;
            u.Postcode = Postcode;
            u.Province = Province;
            u.Phone = Phone;

            u.Total_miles = 0;
            u.Current_miles = 0;

            u.SetUnhashedPassword(Password);

            return u;
        }
    }


    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

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