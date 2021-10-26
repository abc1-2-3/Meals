using Meals.Models.Context;
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
        public ResultObj RemoveItem(Item entity);
    }
}
