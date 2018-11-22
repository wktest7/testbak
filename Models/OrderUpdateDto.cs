using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;

namespace TestApiBakery.Models
{
    public class OrderUpdateDto
    {
        //public decimal FinalPrice { get; set; }
        public int OrderId { get; set; }
        public Status Status { get; set; }

        public List<OrderItemUpdateDto> OrderItems { get; set; } //w service off
    }
}
