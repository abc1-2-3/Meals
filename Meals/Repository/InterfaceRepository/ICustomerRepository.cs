using Meals.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface ICustomerRepository
    {
        public ResultObj SetUpCustomer(Customer entity);
        public ResultObj ModifyCustomer(Customer entity);
    }
}
