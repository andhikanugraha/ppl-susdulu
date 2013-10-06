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

        public Ticket(int ID, int ID_user, int ID_flight, string Email, string First_name, string Middle_name, string Last_name, string Address, string Phone, string Gender, string City, string Province, string Postcode, string Class, int Price, string Seat)
        {
            this.ID = ID;
            this.ID_user = ID_user;
            this.Email = Email;
            this.First_name = First_name;
            this.Middle_name = Middle_name;
            this.Last_name = Last_name;
            this.Address = Address;
            this.Phone = Phone;
            this.Gender = Gender;
            this.City = City;
            this.Province = Province;
            this.Postcode = Postcode;
            this.Class = Class;
            this.Price = Price;
            this.Seat = Seat;
        }
    }
}