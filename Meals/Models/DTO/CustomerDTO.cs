using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string CustomerAccount { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
