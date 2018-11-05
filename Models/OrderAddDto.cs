using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderAddDto
    {
        public List<OrderItemAddDto> OrderItems { get; set; }
    }
}
