using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public ICollection<Product> Products { get; set; }
        = new List<Product>();
    }
}
