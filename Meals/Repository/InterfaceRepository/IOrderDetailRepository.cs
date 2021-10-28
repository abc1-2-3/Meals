using Meals.Models.Context;
using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IOrderDetailRepository
    {
        public ResultObj CreateOrderDetail(List<OrderDetail> entity);
        public ResultObj ModifyOrderDetail(List<OrderDetail> entity);
        public ResultObj CancelOrderDetail(string orderid);
        public ResultObj ModifyStatusOrderDetail(OrderSratusDTO entity);
        public ResultObj FTPModifyStatusOrderDetail(List<int> entity);
    }
}
