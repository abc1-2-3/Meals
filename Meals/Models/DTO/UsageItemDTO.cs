using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class UsageItemDTO
    {
        public int ItemStock { get; set; }
        public int ItemUsageAmount { get; set; }
        public bool enough { get; set; }
    }
}
