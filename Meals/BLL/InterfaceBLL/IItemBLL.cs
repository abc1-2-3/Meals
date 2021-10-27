using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface IItemBLL
    {
        public ResultObj CreateItem(ItemDTO entity);
        public ResultObj ModifyItem(ItemDTO entity);
        public ResultObj RemoveItem(int entity);
    }
}

