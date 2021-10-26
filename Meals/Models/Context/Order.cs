using System;
using System.Collections.Generic;

#nullable disable

namespace Meals.Models.Context
{
    public partial class Order
    {
        public string OrderId { get; set; }
        public string OrderSubject { get; set; }
        public int TableNumber { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public int OrderPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
