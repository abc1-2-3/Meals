using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class FTPJobDTO
    {
        public string OrderNo { get; set; }
        public string CustomerID { get; set; }
        public int TotalAmount { get; set; }
        public string Status { get; set; }

    }
}
