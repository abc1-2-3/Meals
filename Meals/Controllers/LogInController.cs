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
    public class LogInController : ControllerBase
    {
        private readonly ILogInBLL _LogIn;
        public LogInController(ILogInBLL logIn)
        {
            _LogIn = logIn;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj> LogIn(LogInDTO entity)
        {
            return await Task.Run(() => _LogIn.LogIn(entity));
        }
    }
}
