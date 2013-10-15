﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class Payment
    {
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string CreditCardNum { get; set; }
    }
}