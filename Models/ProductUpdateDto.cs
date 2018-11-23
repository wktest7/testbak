using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class ProductUpdateDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        [Range(0, 3000)]
        public string Weight { get; set; }
        [Required]
        [Range(0, 1000)]
        //[RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Price can't have more than 2 decimal places")]
        public decimal Price { get; set; }
        [Required]
        public bool IsHidden { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
