using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _context;
        public OrderRepository(DBContext context)
        {
            _context = context;
        }

        public ResultObj CancelOrder(string orderid)
        {
            var order=_context.Orders.Where(x => x.OrderId == orderid).FirstOrDefault();
            order.OrderStatus = "取消";
            var save=_context.SaveChanges();
            ResultObj result = new ResultObj();
            if (save > 0)
            {

                result.Result = true;
                result.Message = "取消成功";
                result.Key = orderid;
                
            }
            else result.Message = "失敗";
            return result;
        }

        public ResultObj CreateOrder(Order entity)
        {
            _context.Add(entity);
            var r = _context.SaveChanges();
            ResultObj result = new ResultObj();
            if (r > 0)
            {

                result.Result = true;
                result.Message = "建立成功";
                result.Key = entity.OrderId;
                return result;
            }
            else
            {
                result.Message = "建立失敗";
                return result;
            }
        }

        public ResultObj ModifyOrder(Order entity)
        {
            var order = _context.Orders.Where(x => x.OrderId == entity.OrderId).FirstOrDefault();
            order.OrderPrice = entity.OrderPrice;
            order.OrderStatus = entity.OrderStatus;
            order.OrderSubject = entity.OrderSubject;
            order.TableNumber = entity.TableNumber;
            order.ModifyDate = entity.ModifyDate;
            order.CustomerId = entity.CustomerId;



            ResultObj result = new ResultObj();
            var save = _context.SaveChanges();
            if (save > 0) { result.Result = true;
                result.Message = "修改成功";
            }
            else result.Message = "修改失敗";
            return result;
        }

        public ResultObj ModifyStatusOrder(OrderSratusDTO entity)
        {
            var order = _context.Orders.Where(x => x.OrderId == entity.OrderId).FirstOrDefault();
            order.ModifyDate = entity.ModifyDate;
            order.OrderStatus = entity.Status;
            ResultObj result = new ResultObj();
            var save = _context.SaveChanges();
            if (save > 0) result.Result = true;
            else result.Message = "修改失敗";
            return result;
        }
        public ResultObj FTPModifyStatusOrder(string entity)
        {
            var order = _context.Orders.Where(x => x.OrderId == entity).FirstOrDefault();
            order.ModifyDate = DateTime.Now;
            order.OrderStatus ="完成";
            ResultObj result = new ResultObj();
            var save = _context.SaveChanges();
            if (save > 0) result.Result = true;
            else result.Message = "修改失敗";
            return result;
        }
    }
}
