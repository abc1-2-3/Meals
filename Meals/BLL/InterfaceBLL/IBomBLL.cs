using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface IBomBLL
    {
        public ResultObj CreateBom(List<BomDTO> entity);
        public ResultObj ModifyBom(List<BomDTO> entity);
        public ResultObj DeleteBom(int entity);
    }
}
