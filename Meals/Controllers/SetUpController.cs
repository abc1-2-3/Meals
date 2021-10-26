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
    public class SetUpController : ControllerBase
    {
        private readonly ISetUpBLL _setup;
        public SetUpController(ISetUpBLL setup)
        {
            _setup = setup;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResultObj> SetUpCustomer(CustomerDTO entity)
        {
            return await Task.Run(() => _setup.SetUpCustomer(entity));
        }
    }
}
