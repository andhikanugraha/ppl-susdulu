using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("for")]
    public class For
    {
        [Key, Column(Order = 0)]
        public string Id_ticket { get; set; }
        [Key, Column(Order = 1)]
        public string Id_flight { get; set; }
    }
}