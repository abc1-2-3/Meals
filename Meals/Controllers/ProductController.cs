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

    public class ProductController : ControllerBase
    {
        private readonly IProductBLL _product;
        public ProductController(IProductBLL product)
        {
            _product = product;
        }
        [HttpPost]
        public ResultObj CreateProduct(ProductDTO entity)
        {
            return _product.CreateProduct(entity);
        }
        [HttpPut]
        public ResultObj ModifyProduct(ProductDTO entity)
        {
            return _product.ModifyProduct(entity);
        }

        [HttpDelete]
        public ResultObj RemoveProduct(int ProductId)
        {
            return _product.RemoveProduct(ProductId);
        }
        [HttpPut]
        public ResultObj CancelProduct(int ProductId)
        {
            return _product.CancelProduct(ProductId);
        }
    }
}
