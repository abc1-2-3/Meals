using System;
using System.Collections.Generic;

#nullable disable

namespace Meals.Models.Context
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
