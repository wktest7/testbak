using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderItemAddDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }
    }
}
