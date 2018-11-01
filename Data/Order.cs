using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{
    public class Order
    {
        public int OrderId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int BakeryDetailsId { get; set; }
        public BakeryDetails BakeryDetails { get; set; }
    }
}
