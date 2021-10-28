using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DBContext _context;
        public ItemRepository(DBContext context)
        {
            _context = context;
        }

        public ResultObj ConsumeItem(List<ComsumeItemDTO> entity)
        {
            
            foreach(var item in entity)
            {
                var items=Item().Where(x => x.ItemId == item.ItemId).FirstOrDefault();
                items.ItemStock = item.ItemStock;
                items.ModifyDate = item.ModifyDate;
            }
            ResultObj result = new ResultObj();
            var save = _context.SaveChanges();
            if (save > 0)
            {
                result.Result = true;
                result.Message = "消耗物料成功";
            }
            else result.Message = "消耗物料失敗";

            return result;

        }

        public ResultObj CreateItem(Item entity)
        {
            _context.Add(entity);
            var save = _context.SaveChanges();
            ResultObj result = new ResultObj();
            if (save > 0)
            {
                result.Key = entity.ItemId.ToString();
                result.Result = true;
                result.Message = "建立成功";

            }
            else result.Message = "建立失敗";
            return result;
        }

        public IQueryable<Item> Item()
        {
            return _context.Items;
        }

        public ResultObj ModifyItem(Item entity)
        {
            var item = Item().Where(x => x.ItemId == entity.ItemId).FirstOrDefault();
            item.ItemName = entity.ItemName;
            item.ItemStock = entity.ItemStock;
            item.ModifyDate = entity.ModifyDate;

            var save = _context.SaveChanges();

            ResultObj result = new ResultObj();
            if (save > 0)
            {

                result.Result = true;
                result.Message = "修改成功";
                result.Key = item.ItemId.ToString();
            }
            else { result.Message = "修改失敗";  }
            return result;
        }

        public ResultObj RemoveItem(int entity)
        {
            ResultObj result = new ResultObj();
            _context.Remove(Item().Where(x => x.ItemId == entity).FirstOrDefault());
            var save = _context.SaveChanges();

            if (save > 0)
            {
                result.Result = true;
                result.Message = "刪除成功";
            }
            else   result.Message = "刪除失敗";
            return result;

        }
    }
}
