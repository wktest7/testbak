using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public decimal Price { get; set; }
        public bool IsHidden { get; set; }
        public int CategoryId { get; set; }
    }
}
