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
        public async Task<PageOrderDTO> SerchOrder(string status, int? page)
        {
            return await Task.Run(() => _serch.SerchOrder(status,page));
        }

        
        [HttpGet]
        public async Task<PageProductlDTO> SerchDetail(string Type, int? page)
        {
            return await Task.Run(() => _serch.SerchProduct(Type, page));
        }
    }
}
