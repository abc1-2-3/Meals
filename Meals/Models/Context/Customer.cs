using System;
using System.Collections.Generic;

#nullable disable

namespace Meals.Models.Context
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerAccount { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
