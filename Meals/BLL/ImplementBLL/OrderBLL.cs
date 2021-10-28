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
        private readonly Mail _mail;
        public OrderBLL(Mail mail,IItemRepository item,ILogger<OrderBLL> logger, DBContext context, IOrderRepository order, IOrderDetailRepository orderDetail) : base(logger)
        {
            _mail = mail;
            _orderDetail = orderDetail;
            _context = context;
            _order = order;
            _item = item;
        }

        public ResultObj AdditionOrder(OrderDTO entity)
        {
            var result = new ResultObj();
            //加點，可增加訂單明細的餐點，但是訂單狀態為處理中或成立，且加點餐點的物料足夠時才可以加點 
            try
            {
                foreach (var detail in entity.OrderDetails)
                {
                    var stock = from b in _context.Boms.Where(x => x.ProductId == detail.ProductId).ToList()
                                join i in _context.Items.ToList() on b.ItemId equals i.ItemId
                                select new UsageItemDTO
                                {
                                    enough = (i.ItemStock - detail.Amount * b.ItemUsageAmount) > 0
                                };
                    if (!stock.FirstOrDefault().enough)
                    {
                        result.Message = "無法提供此餐點";
                        _logger.LogError("物料不足");
                        return result;
                    }
                }


                var order = _context.Orders.Where(x => x.OrderId == entity.OrderId && (x.OrderStatus == "處理中" || x.OrderStatus == "成立"));
                if (order.Count() > 0)
                {
                    var list = new List<OrderDetail>(entity.OrderDetails.Select(x => new OrderDetail()
                    {
                        CreateDate = DateTime.Now,
                        OrderId = entity.OrderId,
                        ProductId = x.ProductId,
                        Amount = x.Amount,
                        Status = "成立"
                    }));
                    result = _orderDetail.CreateOrderDetail(list);

                }
                else result.Message = "訂單狀態為處理中或成立時才可加點";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return result;
        }

        public ResultObj CancelOrder(string entity)
        {
            var result =new ResultObj();
            try
            {
                var order = _context.Orders.Where(x => x.OrderId == entity && x.OrderStatus == "成立");
                if (order.Count() > 0)
                {
                    result = _order.CancelOrder(entity);
                    if (result.Result)
                        result = _orderDetail.CancelOrderDetail(entity);
                    _logger.LogDebug("CancelOrder OK");
                }
                else result.Message = "訂單狀態為成立時才可取消";
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
            }
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
                        _logger.LogError("物料不足");
                        return result;
                    }
                }

                var order = from p in _context.Products.ToList()
                            join d in entity.OrderDetails on p.ProductId equals d.ProductId
                            group new { p, d } by 1 into g
                            select new Order()
                            {
                                OrderPrice = g.Sum(x => x.d.Amount * x.p.ProductPrice),
                                CreateDate = DateTime.Now,
                                OrderId = number,
                                CustomerId = entity.CustomerId,
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
                    Status = "成立"
                }));

                result = _orderDetail.CreateOrderDetail(list);

                EMail Email = new EMail();
                Email.Subject = "客戶訂單成立";
                Email.Text = "訂單" + number + "成立";
                Email.ToMail = "LiAn.Li@beltom.com.tw";

                _mail.EMail(Email);
                _logger.LogInformation("CreateOrder OK");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return result;
        }
           
        

        public ResultObj ModifyStatusOrder(OrderSratusDTO entity)
        {
            ResultObj result = new ResultObj();
            try
            {

                //List<ComsumeItemDTO> comsumeItems = new List<ComsumeItemDTO>();
                var detail = _context.OrderDetails.Where(x => x.OrderId == entity.OrderId);
                foreach (var od in detail.ToList())
                {
                    var stock = from d in detail.ToList()
                                join b in _context.Boms.ToList() on d.ProductId equals b.ProductId
                                join i in _context.Items.ToList() on b.ItemId equals i.ItemId                                
                                select new ComsumeItemDTO
                                {
                                    ItemId = i.ItemId,
                                    ItemStock = i.ItemStock - d.Amount * b.ItemUsageAmount,
                                    ModifyDate = DateTime.Now
                                };
                    var comsume = stock.ToList();
                    result = _item.ConsumeItem(comsume);
                }
               

                if (result.Result == false) return result;

                result = _order.ModifyStatusOrder(entity);
                result = _orderDetail.ModifyStatusOrderDetail(entity);
                _logger.LogInformation("ModifyStatusOrder OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return result;

            
        }
        public ResultObj ModifyOrderDetail(OrderDTO entity)
        {
            ResultObj result = new ResultObj();
            try
            {
                //修改餐點明細，需檢查該餐點明細的狀態是否為成立，成立才可以修改 
                var order = _context.OrderDetails.Where(x => x.OrderId == entity.OrderId && x.Status == "成立" );
                if (order.Count() > 0)
                {
                    List<OrderDetail> detail = new List<OrderDetail>();
                    foreach (var details in entity.OrderDetails)
                    {
                        if (_context.OrderDetails.Where(x => x.OrderDetailId == details.OrderDetailId).Count() > 0)
                        {
                            OrderDetail detail1 = new OrderDetail()
                            {
                                ModifyDate = DateTime.Now,
                                Amount = details.Amount,
                                OrderDetailId = details.OrderDetailId,
                                ProductId = details.ProductId
                            };
                            detail.Add(detail1);
                        }
                        else
                        {
                            _logger.LogInformation("修改的OrderdetaiID沒有");
                            return result;
                        }
                        
                    };
                    result = _orderDetail.ModifyOrderDetail(detail);

                    var order2 = from p in _context.Products.ToList()
                                 join d in _context.OrderDetails.Where(x=>x.OrderId==entity.OrderId) on p.ProductId equals d.ProductId
                                 group new { p, d } by 1 into g
                                 select new Order()
                                 {
                                     OrderId=entity.OrderId,
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return result;

        }


        public ResultObj FTPModifyStatusOrder(string entity)
        {
            ResultObj result = new ResultObj();
            try
            {
                var order = _context.OrderDetails.Where(x => x.OrderId == entity).Select(x=>x.OrderDetailId).ToList();
                result = _order.FTPModifyStatusOrder(entity);
                result = _orderDetail.FTPModifyStatusOrderDetail(order);

                _logger.LogInformation("Update CompleteOrder OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return result;


        }
    }
}
