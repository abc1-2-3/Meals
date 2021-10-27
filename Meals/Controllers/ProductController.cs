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
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class ProductController : ControllerBase
    {
        private readonly IProductBLL _product;
        public ProductController(IProductBLL product)
        {
            _product = product;
        }
        [HttpPost]
        public async Task<ResultObj> CreateProduct(ProductDTO entity)
        {
            return await Task.Run(() => _product.CreateProduct(entity));
        }
        [HttpPut]
        public async Task<ResultObj> ModifyProduct(ProductDTO entity)
        {
            return await Task.Run(() => _product.ModifyProduct(entity));
        }

        [HttpDelete]
        public async Task<ResultObj> RemoveProduct(int ProductId)
        {
            return await Task.Run(() => _product.RemoveProduct(ProductId));
        }
        [HttpPut]
        public async Task<ResultObj> CancelProduct(int ProductId)
        {
            return await Task.Run(() => _product.CancelProduct(ProductId));
        }
    }
}
