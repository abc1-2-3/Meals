using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface ISerchBLL
    {
        public PageOrderDTO SerchOrder(int? page, string status = null);
        public PageProductlDTO SerchProduct(int? page, string Type );

        public List<SerchOrder2DTO> SerchOrderQuartz(string Type = null);
    }
}
