using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderItemUpdateDto
    {
        [Required]
        public int OrderItemId { get; set; }
        [Required]
        [Range(0, 1000)]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }
    }
}
