using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Nip { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
