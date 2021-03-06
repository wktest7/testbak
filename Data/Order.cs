﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }

    public enum Status : int
    {
        [Display(Name = "New")]
        New,

        [Display(Name = "Shipped")]
        Shipped
    }
}
