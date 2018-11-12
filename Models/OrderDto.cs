using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderDto
    {
        //public string CompanyName { get; set; }
        //public string CompanyAddress { get; set; }
        //public string CompanyPostalCode { get; set; }
        //public string CompanyNip { get; set; }
        //public string CompanyPhone { get; set; }

        //public string CustomerName { get; set; }
        //public string CustomerAddress { get; set; }
        //public string CustomerPostalCode { get; set; }
        //public string CustomerNip { get; set; }
        //public string CustomerPhone { get; set; }

        public decimal FinalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }


        public List<OrderItemDto> OrderItems { get; set; }

    }
}
