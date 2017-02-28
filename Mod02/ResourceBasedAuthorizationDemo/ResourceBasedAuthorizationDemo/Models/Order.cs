using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceBasedAuthenticationDemo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
    }
}
