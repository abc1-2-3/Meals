using Meals.BLL.InterfaceBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.ImplementBLL
{
    public class ItemBLL : BaseBLL,IItemBLL
    {
        private readonly IItemRepository _item;
        private readonly DBContext _context;
        public ItemBLL(ILogger<ItemBLL> logger, DBContext context, IItemRepository item) : base(logger)
        {
            _context = context;
            _item = item;
        }
        public ResultObj CreateItem(ItemDTO entity)
        {
            Item item = new Item()
            {
                CreateDate = DateTime.Now,
                ItemName=entity.ItemName,
                ItemStock=entity.ItemStock
            };

            var result = _item.CreateItem(item);


            _logger.LogDebug("OK"+ result.Key);

            return result;
        }

        public ResultObj ModifyItem(ItemDTO entity)
        {
            Item item = new Item()
            {
                ItemName = entity.ItemName,
                ItemStock = entity.ItemStock,
                ModifyDate=DateTime.Now,
                ItemId=entity.ItemId
            };
            var result = _item.ModifyItem(item);

            _logger.LogDebug(" ModifyItem OK");
            return result;
        }

        public ResultObj RemoveItem(int entity)
        {
            ResultObj result = new ResultObj();
            if (_context.Boms.Where(x => x.ItemId == entity).Count() < 1 )
            {
                result = _item.RemoveItem(entity);
                _logger.LogDebug("OK");
            }
            else
            {
                result.Message = "關聯到BOM不可刪除 ";
                _logger.LogInformation("關聯到BOM不可刪除");
            }
            return result;
        }
    }
}
