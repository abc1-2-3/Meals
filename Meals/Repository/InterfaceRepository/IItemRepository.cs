using Meals.Models.Context;
using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IItemRepository
    {
        public IQueryable<Item> Item();
        public ResultObj CreateItem(Item entity);
        public ResultObj ModifyItem(Item entity);
        public ResultObj RemoveItem(int entity);

        public ResultObj ConsumeItem(List<ComsumeItemDTO> entity);
    }
}
