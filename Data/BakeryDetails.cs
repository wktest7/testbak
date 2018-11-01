using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{
    public class BakeryDetails
    {
        public int BakeryDetailsId { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
    }
}
