using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class OrderDTO
    {
        public string OrderId { get; set; }
        public string OrderSubject { get; set; }
        public int TableNumber { get; set; }
        public string CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public int OrderPrice { get; set; }
    }
}
