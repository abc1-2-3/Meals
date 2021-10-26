using Meals.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IOrderRepository
    {
        public ResultObj CreateOrder(Order entity);
    }
}
