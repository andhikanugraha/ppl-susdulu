using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("airport")]
    public class Airport
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}