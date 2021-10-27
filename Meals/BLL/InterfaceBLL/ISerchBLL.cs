using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface ISerchBLL
    {
        public PageOrderDTO SerchOrder(string status, int? page);
        public PageProductlDTO SerchProduct(string Type, int? page);
    }
}
