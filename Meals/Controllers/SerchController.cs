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
    [Authorize]
    public class SerchController : ControllerBase
    {
        private readonly ISerchBLL _serch;
        public SerchController(ISerchBLL serch)
        {
            _serch = serch;
        }

        [HttpGet]
        public PageOrderDTO SerchOrder(string status, int? page)
        {
            return _serch.SerchOrder(status,page);
        }

        
        [HttpGet]
        public PageProductlDTO SerchDetail(string Type, int? page)
        {
            return _serch.SerchProduct(Type, page);
        }
    }
}
