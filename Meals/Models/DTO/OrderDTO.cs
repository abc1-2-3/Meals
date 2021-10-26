using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Models.DTO
{
    public class OrderDTO
    {
        public string OrderId { get; set; }
        public string OrderSubject { get; set; }
        public int TableNumber { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public int OrderPrice { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
    public partial class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }

    }
}
