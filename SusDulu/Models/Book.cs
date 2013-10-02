using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("book")]
    public class Book
    {
        [Key, Column(Order = 0)] 
        public string Id_ticket { get; set; }
        [Key, Column(Order = 1)]
        public string Id_user { get; set; }
    }
}