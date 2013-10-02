using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class FlightTicket
    {
        public Flight flight { get; set; }
        public Ticket ticket { get; set; }
    }
}