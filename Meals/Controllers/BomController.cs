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
        public ResultObj CreateBom(List<BomDTO> entity)
        {
            return _bom.CreateBom(entity);
        }
        [HttpPut]
        public ResultObj ModifyBom(List<BomDTO> entity)
        {
            return _bom.ModifyBom(entity);
        }

        [HttpDelete]
        public ResultObj DeleteBom(int BomAutoId)
        {
            return _bom.DeleteBom(BomAutoId);
        }
    }
}
