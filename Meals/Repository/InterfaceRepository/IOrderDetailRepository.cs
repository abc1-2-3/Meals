using Meals.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IOrderDetailRepository
    {
        public ResultObj CreateOrderDetail(OrderDetail entity);
    }
}
