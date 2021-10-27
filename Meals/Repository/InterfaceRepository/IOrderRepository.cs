using Meals.Models.Context;
using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IOrderRepository
    {
        public ResultObj CreateOrder(Order entity);
        public ResultObj ModifyOrder(Order entity);
        public ResultObj CancelOrder(string orderid);
        public ResultObj ModifyStatusOrder(OrderSratusDTO entity);
    }
}
