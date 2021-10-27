using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class BomDTO
    {
        public int AutoId { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public int ItemUsageAmount { get; set; }
    }
}
