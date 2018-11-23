using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;

namespace TestApiBakery.Models
{
    public class OrderUpdateDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
