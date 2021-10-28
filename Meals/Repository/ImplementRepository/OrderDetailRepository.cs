using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DBContext _context;
        public OrderDetailRepository(DBContext context)
        {
            _context = context;
        }


        public ResultObj CancelOrderDetail(string orderid)
        {
            var orderdetail=_context.OrderDetails.Where(x => x.OrderId == orderid);
            foreach(var details in orderdetail)
            {
                details.Status = "取消";
            }
            var save = _context.SaveChanges();
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

        public ResultObj CreateOrderDetail(List<OrderDetail> entity)
        {
            ResultObj result = new ResultObj();
            _context.AddRange(entity);
            var save = _context.SaveChanges();
            if (save > 0) { 
                result.Result = true;
                result.Message = "成功";
            }
            else
            {
                result.Message = "失敗";
            };
            return result;
        }

        public ResultObj ModifyOrderDetail(List<OrderDetail> entity)
        {
            ResultObj result = new ResultObj();


            foreach (OrderDetail detail in entity)
            {
                var details = _context.OrderDetails.Where(x => x.OrderDetailId == detail.OrderDetailId).FirstOrDefault();
                details.ModifyDate = detail.ModifyDate;
                details.ProductId = detail.ProductId;
                details.Amount = detail.Amount;
              
            }
            var save = _context.SaveChanges();
            if (save > 0) { result.Result = true;
                result.Message = "修改成功";
            }
            else result.Message = "修改失敗";
            
            return result;
        }
        public ResultObj ModifyStatusOrderDetail(OrderSratusDTO entity)
        {
            var order = _context.OrderDetails.Where(x => x.OrderId == entity.OrderId).ToList();
            foreach (var detail in order) {
                detail.ModifyDate = entity.ModifyDate;
                detail.Status = entity.Status; 
            }
            ResultObj result = new ResultObj();
            var save = _context.SaveChanges();
            if (save > 0)
            {
                result.Result = true;
                result.Message = "修改成功";
            }
            else result.Message = "修改失敗";
            return result;
        }
        public ResultObj FTPModifyStatusOrderDetail(List<int> entity)
        {
            for(int i = 0; i < entity.Count; i++)
            {
                var detail=_context.OrderDetails.Where(x => x.OrderDetailId == entity[i]).FirstOrDefault();
                detail.ModifyDate = DateTime.Now;
                detail.Status = "完成";
            }
            
            ResultObj result = new ResultObj();
            var save = _context.SaveChanges();
            if (save > 0)
            {
                result.Result = true;
                result.Message = "修改成功";
            }
            else result.Message = "修改失敗";
            return result;
        }
    }
}
