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
    public class Order2DTO
    {
        public string OrderSubject { get; set; }
        public int TableNumber { get; set; }
        public int CustomerId { get; set; }
        public int OrderPrice { get; set; }
        public List<OrderDetail2DTO> OrderDetails { get; set; }
    }
    public partial class OrderDetail2DTO
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }

    }
    public partial class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }

    }
    public partial class OrderSratusDTO
    {

        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }
        public DateTime? ModifyDate { get; set; }

    }
    public class SerchOrderDTO
    {
        public string OrderId { get; set; }
        public string OrderSubject { get; set; }
        public int TableNumber { get; set; }
        public int CustomerId { get; set; }
        public string OrderStatus { get; set; }
        public int OrderPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }


}
