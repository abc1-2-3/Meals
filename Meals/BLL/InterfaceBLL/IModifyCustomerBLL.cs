using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface IModifyCustomerBLL
    {
        public ResultObj ModifyCustomer(CustomerDTO entity);
    }
}
