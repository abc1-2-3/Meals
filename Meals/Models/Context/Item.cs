using System;
using System.Collections.Generic;

#nullable disable

namespace Meals.Models.Context
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemStock { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
