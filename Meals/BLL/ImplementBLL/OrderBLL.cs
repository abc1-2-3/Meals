using Meals.BLL.InterfaceBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.ImplementBLL
{
    public class OrderBLL : BaseBLL, IOrderBLL
    {
        private readonly IOrderRepository _order;
        private readonly IOrderDetailRepository _orderdetail;
        private readonly DBContext _context;
        public OrderBLL(ILogger<OrderBLL> logger, DBContext context, IOrderRepository order, IOrderDetailRepository orderDetail) : base(logger)
        {
            _orderdetail = orderDetail;
            _context = context;
            _order = order;
        }
    {
        public ResultObj CancelOrder(int entity)
        {
            throw new NotImplementedException();
        }

        public ResultObj CreateOrder(OrderDTO entity)
        {
            ResultObj result = new ResultObj();
            int start = 1;
            string number = "";
            if (_context.Orders.Count() < 1)
                number = "O"+DateTime.Now.ToString("yyyyMMdd") + start.ToString("000");
            else if (_context.Orders.Count() >998 ) { number ="O"+ DateTime.Now.ToString("yyyyMMdd") + (_context.Orders.Count()%998 + 1).ToString("000"); }
            else number = "O"+DateTime.Now.ToString("yyyyMMdd")+(_context.Orders.Count() + 1).ToString("000");
            
            foreach(var detail in entity.OrderDetails)
            {
                var stock=from b in _context.Boms.Where(x => x.ProductId == detail.ProductId)
                join i in _context.Items on b.ItemId equals i.ItemId
                select new UsageItemDTO { 
                    enough=(i.ItemStock- detail.Amount * b.ItemUsageAmount)>0
                };
                if (!stock.FirstOrDefault().enough)
                {
                    result.Message = "無法提供此餐點";
                    return result;
                }
            }

            Order order = new Order()
            {
                CreateDate = DateTime.Now,
                OrderId = number,
                CustomerId=entity.CustomerId,
                OrderPrice=entity.OrderPrice,
                OrderStatus=entity.OrderStatus,
                OrderSubject=entity.OrderSubject,
                TableNumber=entity.TableNumber
            };
            result = _order.CreateOrder(order);
            if (result.Result == false) return result;
            var list = new List<OrderDetail>(entity.OrderDetails.Select(x => new OrderDetail()
            {
                CreateDate = DateTime.Now,
                OrderId = order.OrderId,
                ProductId = x.ProductId,
                Amount = x.Amount,
                Status = x.Status
            }));

            result = _orderDetail.CreateOrderDetail(list);
            return result;
        }
           
        

        public ResultObj ModifyOrder(OrderDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
