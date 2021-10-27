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
        private readonly IOrderDetailRepository _orderDetail;
        private readonly DBContext _context;
        private readonly IItemRepository _item;
        public OrderBLL(IItemRepository item,ILogger<OrderBLL> logger, DBContext context, IOrderRepository order, IOrderDetailRepository orderDetail) : base(logger)
        {
            _orderDetail = orderDetail;
            _context = context;
            _order = order;
            _item = item;
        }

        public ResultObj AdditionOrder(OrderDTO entity)
        {
            var result = new ResultObj();
            //加點，可增加訂單明細的餐點，但是訂單狀態為處理中或成立，且加點餐點的物料足夠時才可以加點 

            foreach (var detail in entity.OrderDetails)
            {
                var stock = from b in _context.Boms.Where(x => x.ProductId == detail.ProductId)
                            join i in _context.Items on b.ItemId equals i.ItemId
                            select new UsageItemDTO
                            {
                                enough = (i.ItemStock - detail.Amount * b.ItemUsageAmount) > 0
                            };
                if (!stock.FirstOrDefault().enough)
                {
                    result.Message = "無法提供此餐點";
                    return result;
                }
            }


            var order= _context.Orders.Where(x => x.OrderId == entity.OrderId && (x.OrderStatus == "處理中" || x.OrderStatus == "成立"));
            if (order.Count() > 0)
            {
                var list = new List<OrderDetail>(entity.OrderDetails.Select(x => new OrderDetail()
                {
                    CreateDate = DateTime.Now,
                    OrderId = entity.OrderId,
                    ProductId = x.ProductId,
                    Amount = x.Amount,
                    Status = x.Status
                }));
                result = _orderDetail.CreateOrderDetail(list);

            }
            else result.Message = "訂單狀態為處理中或成立時才可加點";
            return result;
        }

        public ResultObj CancelOrder(string entity)
        {
            var result =new ResultObj();
            var order=_context.Orders.Where(x => x.OrderId == entity&&x.OrderStatus=="成立");
            if (order.Count() > 0)
            {
                result = _order.CancelOrder(entity);
                if (result.Result)
                    result = _orderDetail.CancelOrderDetail(entity);
            }
            else result.Message = "訂單狀態為成立時才可取消";
            return result;
        }

        public ResultObj CreateOrder(Order2DTO entity)
        {
            ResultObj result = new ResultObj();
            int start = 1;
            string number = "";
            if (_context.Orders.Count() < 1)
                number = "O"+DateTime.Now.ToString("yyyyMMdd") + start.ToString("000");
            else if (_context.Orders.Count() >998 ) { number ="O"+ DateTime.Now.ToString("yyyyMMdd") + (_context.Orders.Count()%998 + 1).ToString("000"); }
            else number = "O"+DateTime.Now.ToString("yyyyMMdd")+(_context.Orders.Count() + 1).ToString("000");
            try
            {
                foreach (var detail in entity.OrderDetails)
                {
                    var stock = from b in _context.Boms.Where(x => x.ProductId == detail.ProductId)
                                join i in _context.Items on b.ItemId equals i.ItemId
                                select new
                                {
                                    enough = (i.ItemStock - detail.Amount * b.ItemUsageAmount) > 0
                                };

                    if (!stock.Select(x=>x.enough).FirstOrDefault())
                    {
                        result.Message = "無法提供此餐點";
                        return result;
                    }
                }

                var order = from p in _context.Products
                            join d in _context.OrderDetails on p.ProductId equals d.ProductId
                            group new { p, d } by 1 into g
                            select new Order()
                            {
                                OrderPrice = g.Sum(x => x.d.Amount * x.p.ProductPrice),
                            };
                var order3 = order.FirstOrDefault();
                Order order2 = new Order()
                {
                    CreateDate = DateTime.Now,
                    OrderId = number,
                    CustomerId = entity.CustomerId,
                    OrderPrice = order3.OrderPrice,
                    OrderStatus = "成立",
                    OrderSubject = entity.OrderSubject,
                    TableNumber = entity.TableNumber
                };
                result = _order.CreateOrder(order.FirstOrDefault());
                if (result.Result == false) return result;


                var list = new List<OrderDetail>(entity.OrderDetails.Select(x => new OrderDetail()
                {
                    CreateDate = DateTime.Now,
                    OrderId = order.FirstOrDefault().OrderId,
                    ProductId = x.ProductId,
                    Amount = x.Amount,
                    Status = x.Status
                }));

                result = _orderDetail.CreateOrderDetail(list);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return result;
        }
           
        

        public ResultObj ModifyStatusOrder(OrderSratusDTO entity)
        {
            ResultObj result = new ResultObj();
            OrderSratusDTO order = new OrderSratusDTO()
            {
                ModifyDate = DateTime.Now,
                OrderId = entity.OrderId,
                Status=entity.Status
            };

            List<ComsumeItemDTO> comsumeItems = new List<ComsumeItemDTO>();
            var detail = _context.OrderDetails.Where(x => x.OrderId == entity.OrderId);
            foreach (var od in detail)
            {
                var stock = from b in _context.Boms.Where(x => x.ProductId == od.ProductId)
                            join i in _context.Items on b.ItemId equals i.ItemId
                            select new ComsumeItemDTO
                            {
                                ItemId = i.ItemId,
                                ItemStock = i.ItemStock - od.Amount * b.ItemUsageAmount,
                                ModifyDate = DateTime.Now
                            };
                comsumeItems.Add(stock.FirstOrDefault());
            }
            result = _item.ConsumeItem(comsumeItems);

            if (result.Result == false) return result;

            result = _order.ModifyStatusOrder(order);
            result = _orderDetail.ModifyStatusOrderDetail(order);
            return result;

            
        }
        public ResultObj ModifyOrderDetail(OrderDTO entity)
        {
            ResultObj result = new ResultObj();
            //修改餐點明細，需檢查該餐點明細的狀態是否為成立，成立才可以修改 
            var order = _context.OrderDetails.Where(x => x.OrderId == entity.OrderId &&  x.Status == "成立");
            if (order.Count() > 0)
            {
                List<OrderDetail> detail = new List<OrderDetail>();
                foreach (var details in entity.OrderDetails)
                {
                    OrderDetail detail1 = new OrderDetail()
                    {
                        ModifyDate = DateTime.Now,
                        Amount = details.Amount,
                        OrderDetailId = details.OrderDetailId,
                        ProductId = details.ProductId
                    }; detail.Add(detail1);
                };
                result = _orderDetail.ModifyOrderDetail(detail);

                var order2 = from p in _context.Products
                            join d in _context.OrderDetails on p.ProductId equals d.ProductId
                            group new { p, d } by new { d.OrderId } into g
                            select new Order()
                            {
                                CreateDate = DateTime.Now,
                                CustomerId = entity.CustomerId,
                                OrderPrice = g.Sum(x => x.d.Amount * x.p.ProductPrice),
                                OrderStatus = "成立",
                                OrderSubject = entity.OrderSubject,
                                TableNumber = entity.TableNumber
                            };
                result = _order.ModifyOrder(order2.FirstOrDefault());


            }
            else
            {
                result.Message = "狀態成立時才能修改";
            }

            return result;

        }
    }
}
