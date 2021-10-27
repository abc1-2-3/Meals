using Meals.BLL.InterfaceBLL;
using Meals.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    [Authorize]

    public class ItemController : ControllerBase
    {
        private readonly IItemBLL _item;
        public ItemController(IItemBLL item)
        {
            _item = item;
        }
        [HttpPost]
        public ResultObj CreateProduct(ItemDTO entity)
        {
            return _item.CreateItem(entity);
        }
        [HttpPut]
        public ResultObj ModifyProduct(ItemDTO entity)
        {
            return _item.ModifyItem(entity);
        }

        [HttpDelete]
        public ResultObj RemoveProduct(int ItemId)
        {
            return _item.RemoveItem(ItemId);
        }

    }
}
