using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    [Table("use")]
    public class Use
    {
        [Key, Column(Order = 0)] 
        public string Id_flight { get; set; }
        [Key, Column(Order = 1)] 
        public string Id_plane { get; set; }
    }
}