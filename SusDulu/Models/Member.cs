using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SusDulu.Models
{
    public class Member
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Distance_Traveled { get; set; }
    }

    public class MembersContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}