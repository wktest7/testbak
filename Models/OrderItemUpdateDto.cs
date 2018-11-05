using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderItemUpdateDto
    {
        public int OrderItemId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
