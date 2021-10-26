using Meals.Models.Context;
using Meals.Repository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class ItemRepository : IItemRepository
    {
        public ResultObj CreateItem(Item entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Item> Item()
        {
            throw new NotImplementedException();
        }

        public ResultObj ModifyItem(Item entity)
        {
            throw new NotImplementedException();
        }

        public ResultObj RemoveItem(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
