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
    public class ProductBLL : BaseBLL,IProductBLL
    {
        private readonly IProductRepository _product;
        private readonly DBContext _context;
        public ProductBLL(ILogger<ProductBLL> logger,DBContext context,IProductRepository bom):base(logger)
        {
            _context = context;
            _product = bom;
        }

        public ResultObj CancelProduct(int entity)
        {

            var result = _product.CancelProduct(entity);
            return result;
        }

        public ResultObj CreateProduct(ProductDTO entity)
        {
            Product product = new Product()
            {
                ProductName = entity.ProductName,
                ProductStatus = entity.ProductStatus,
                CreateDate = DateTime.Now,
                ProductPrice = entity.ProductPrice,
                ProductType=entity.ProductType
            };

            var result = _product.CreateProduct(product);


            return result;
        }

        public ResultObj ModifyProduct(ProductDTO entity)
        {
            Product product = new Product()
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                ProductPrice = entity.ProductPrice,
                ProductPicture = entity.ProductPicture,
                ProductStatus=entity.ProductStatus,
                ProductType=entity.ProductType,
                ModifyDate = DateTime.Now
            };
            var result = _product.ModifyProduct(product);


            return result;
        }

        public ResultObj RemoveProduct(int entity)
        {
            ResultObj result = new ResultObj();
            if (_context.Boms.Where(x => x.ProductId == entity).Count() < 1 && _context.OrderDetails.Where(x => x.ProductId == entity).Count() < 1)
            {
                result = _product.RemoveProduct(entity);
                
            }
            else
            {
                result.Message= "曾經被下過訂單或是有關連到BOM不可以刪除";
                return result;
            }
            return result;
        }
    }
}
