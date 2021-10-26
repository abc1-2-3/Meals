using System;
using System.Collections.Generic;

#nullable disable

namespace Meals.Models.Context
{
    public partial class Bom
    {
        public int AutoId { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public int ItemUsageAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
