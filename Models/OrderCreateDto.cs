using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
