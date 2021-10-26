using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductStatus { get; set; } = "不可點餐";
        public string ProductPicture { get; set; }
    }
}
