using System;
using System.Collections.Generic;

#nullable disable

namespace Meals.Models.Context
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductStatus { get; set; }
        public string ProductPicture { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
