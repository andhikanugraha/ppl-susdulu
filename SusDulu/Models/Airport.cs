using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("bandara")]
    public class Airport
    {
        [Key, Column("kodebandara")]
        public string ID { get; set; }
        [Column("namabandara")]
        public string Name { get; set; }
    }
}