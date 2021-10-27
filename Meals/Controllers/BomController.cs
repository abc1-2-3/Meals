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
    public class BomController : ControllerBase
    {
        private readonly IBomBLL _bom;
        public BomController(IBomBLL bom)
        {
            _bom = bom;
        }
        [HttpPost]
        public async Task<ResultObj> CreateBom(List<BomDTO> entity)
        {
            return await Task.Run(() => _bom.CreateBom(entity));
        }
        [HttpPut]
        public async Task<ResultObj> ModifyBom(List<BomDTO> entity)
        {
            return await Task.Run(() => _bom.ModifyBom(entity));
        }

        [HttpDelete]
        public async Task<ResultObj> DeleteBom(int BomAutoId)
        {
            return await Task.Run(() => _bom.DeleteBom(BomAutoId));
        }
    }
}
