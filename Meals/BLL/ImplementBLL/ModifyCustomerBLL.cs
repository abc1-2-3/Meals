using Meals.BLL.InterfaceBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.ImplementBLL
{
    public class ModifyCustomerBLL : BaseBLL, IModifyCustomerBLL
    {
        private readonly MD5 _md5;
        private readonly ICustomerRepository _customer;
        public ModifyCustomerBLL(ILogger<ModifyCustomerBLL> logger, MD5 d5, ICustomerRepository customer) : base(logger)
        {
            _md5 = d5;
            _customer = customer;
        }
        public ResultObj ModifyCustomer(CustomerDTO entity)
        {
            var result = new ResultObj();
            try
            {
                string MD5password = _md5.getMd5Method2(entity.CustomerPassword);
                Customer customer = new Customer()
                {
                    CustomerId=entity.CustomerId,
                    ModifyDate=DateTime.Now,
                    CustomerAccount=entity.CustomerAccount,
                    CustomerEmail=entity.CustomerEmail,
                    CustomerName=entity.CustomerName,
                    CustomerPassword= MD5password
                };
                result = _customer.ModifyCustomer(customer);
                _logger.LogInformation("ModifyCustomer OK");
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            return result;
        }
    }
}
