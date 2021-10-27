using Meals.Models.Context;
using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IBomRepository
    {
        public ResultObj CreateBom(List<Bom> entity);
        public ResultObj ModifyBom(List<Bom> entity);
        public ResultObj DeleteBom(int entity);
    }
}
