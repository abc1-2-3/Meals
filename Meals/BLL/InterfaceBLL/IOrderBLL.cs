using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface IOrderBLL
    {
        public ResultObj CreateOrder(OrderDTO entity);
        public ResultObj ModifyOrder(OrderDTO entity);
        public ResultObj CancelOrder(int entity);
    }
}
