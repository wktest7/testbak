﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Models
{
    public class OrderItemAddDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}