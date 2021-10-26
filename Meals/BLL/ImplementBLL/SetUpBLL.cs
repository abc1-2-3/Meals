using Meals.BLL.InterfaceBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.ImplementBLL
{
    public class SetUpBLL : BaseBLL,ISetUpBLL
    {
        private readonly ICustomerRepository _customer;
        private readonly MD5 _md5;
        public SetUpBLL(ILogger<SetUpBLL> logger, ICustomerRepository customer, MD5 d5) :base(logger)
        {
            _customer = customer;
            _md5 = d5;
        }

        public ResultObj SetUpCustomer(CustomerDTO entity)
        {
            var result = new ResultObj();
            try
            {
                string MD5password=_md5.getMd5Method2(entity.CustomerPassword);
                Customer customer = new Customer()
                {
                    CreateDate = DateTime.Now,
                    CustomerAccount = entity.CustomerAccount,
                    CustomerName = entity.CustomerName,
                    CustomerPassword = MD5password,
                    CustomerEmail = entity.CustomerEmail
                };
                result = _customer.SetUpCustomer(customer);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return result;
        }
    }
}
