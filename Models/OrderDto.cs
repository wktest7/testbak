using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
        public string Nip { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
