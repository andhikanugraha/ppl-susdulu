using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("penumpang")]
    public class Ticket
    {
        [Key, Column("kodepenumpang")]
        public int ID { get; set; }
        public int? ID_user { get; set; }
        [Column("kodepenerbangan")]
        public int ID_flight { get; set; }

        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage="E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage="Nama depan harus diisi")]
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }

        [Required(ErrorMessage="Alamat harus diisi")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Nomor telepon harus diisi")]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Kota harus diisi")]
        public string City { get; set; }
        [Required(ErrorMessage = "Provinsi harus diisi")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Kode pos harus diisi")]
        public string Postcode { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int Price { get; set; }
        public string Seat { get; set; }

        public Flight penerbangan;

        public Ticket()
        {
        }

        public Flight getPenerbangan(){
            DefaultConnection db = new DefaultConnection();
            penerbangan = db.Flights.Find(ID_flight);
            return penerbangan;
        }

        public Ticket(int ID, int? ID_user, int ID_flight, string Email, string First_name, string Middle_name, string Last_name, string Address, string Phone, string Gender, string City, string Province, string Postcode, string Class, int Price, string Seat)
        {
            this.ID = ID;
            this.ID_user = ID_user;
            this.ID_flight = ID_flight;
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

        public string GetFullName()
        {
            string fullName = First_name;
            if (Middle_name.Length > 0)
                fullName += " " + Middle_name;
            if (Last_name.Length > 0)
                fullName += " " + Last_name;

            return fullName;
        }

        public Flight GetFlight()
        {
            var db = new DefaultConnection();
            return db.Flights.Find(ID_flight);
        }
    }

    public class FlightOption
    {
        public int id_flight1 { get; set; }
        public int id_flight2 { get; set; }
        public int Sum { get; set; }
    }

    public class FauxTicket
    {
        public string Name;
        public string SeatClass;
        public int Price;
        public string Seat;
    }
}