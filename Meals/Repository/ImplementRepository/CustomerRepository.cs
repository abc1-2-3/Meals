using Meals.Models.Context;
using Meals.Repository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DBContext _dBContext;

        public CustomerRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public ResultObj ModifyCustomer(Customer entity)
        {
            var customer = _dBContext.Customers.Where(x => x.CustomerId == entity.CustomerId).FirstOrDefault();

            customer.CustomerName = entity.CustomerName;
            customer.CustomerPassword = entity.CustomerPassword;
            customer.CustomerEmail = entity.CustomerEmail;
            customer.ModifyDate = entity.ModifyDate;
            customer.CustomerAccount = entity.CustomerAccount;
            
            var save = _dBContext.SaveChanges();
            ResultObj result = new ResultObj();
            if (save > 0) { result.Result = true;
                result.Message = "成功";
                result.Key = customer.CustomerId.ToString();
            }
            else result.Message = "失敗";
            return result;
        }

        public ResultObj SetUpCustomer(Customer entity)
        {
            _dBContext.Add(entity);
            ResultObj result = new ResultObj();
            var r = _dBContext.SaveChanges();
            if (r > 0)
            {
                result.Result = true;
                result.Message = "成功";
                result.Key = entity.CustomerId.ToString();
            }
            else result.Message = "失敗";

            return result;
        }

    }
}
