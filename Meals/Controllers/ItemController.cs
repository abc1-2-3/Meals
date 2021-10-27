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
        public async Task<ResultObj> CreateProduct(ItemDTO entity)
        {
            return await Task.Run(() => _item.CreateItem(entity));
        }
        [HttpPut]
        public async Task<ResultObj> ModifyProduct(ItemDTO entity)
        {
            return await Task.Run(() => _item.ModifyItem(entity));
        }

        [HttpDelete]
        public async Task<ResultObj> RemoveProduct(int ItemId)
        {
            return await Task.Run(() => _item.RemoveItem(ItemId));
        }

    }
}
