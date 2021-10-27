using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemStock { get; set; }
    }
    public class ComsumeItemDTO
    {
        public int ItemId { get; set; }
        public int ItemStock { get; set; }
        public DateTime? ModifyDate { get; set; }

    }
}
