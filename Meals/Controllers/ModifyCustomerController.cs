using Meals.BLL.InterfaceBLL;
using Meals.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModifyCustomerController : ControllerBase
    {
        private readonly IModifyCustomerBLL _modifycustomer;
        public ModifyCustomerController(IModifyCustomerBLL modifycustomer)
        {
            _modifycustomer = modifycustomer;
        }
        [Authorize]
        [HttpPut]
        public async Task<ResultObj> ModifyCustomer(CustomerDTO entity)

        {
            ResultObj result2 = new ResultObj();
            var paylosd = User.Claims.FirstOrDefault(p => p.Type == "CustomerAccount").Value;
            if (paylosd == entity.CustomerAccount)
                return await Task.Run(() => _modifycustomer.ModifyCustomer(entity));
            else return await Task.Run(() => result2);
        }
    }
}
