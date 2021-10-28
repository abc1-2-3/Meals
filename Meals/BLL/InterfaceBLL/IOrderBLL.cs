using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface IOrderBLL
    {
        public ResultObj CreateOrder(Order2DTO entity);
        public ResultObj ModifyStatusOrder(OrderSratusDTO entity);
        public ResultObj CancelOrder(string entity);
        public ResultObj AdditionOrder(OrderDTO entity);
        public ResultObj ModifyOrderDetail(OrderDTO entity);
        public ResultObj FTPModifyStatusOrder(string entity);
    }
}
