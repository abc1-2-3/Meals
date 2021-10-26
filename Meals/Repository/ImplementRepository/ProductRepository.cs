using Meals.Models.Context;
using Meals.Repository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _context;
        public ProductRepository(DBContext context)
        {
            _context = context;
        }
        public ResultObj CreateProduct(Product entity)
        {
            _context.Add(entity);
            var save = _context.SaveChanges();
            ResultObj result = new ResultObj();
            if (save > 0)
            {
                result.Key = entity.ProductId.ToString();
                result.Result = true;
                result.Message = "建立成功";

                return result;
            }
            else
            {

                result.Message = "建立失敗";

                return result;
            }
        }

        public ResultObj ModifyProduct(Product entity)
        {
            var product = Product().Where(x => x.ProductId == entity.ProductId).FirstOrDefault();
            product.ModifyDate = entity.ModifyDate;
            product.ProductName = entity.ProductName;
            product.ProductPicture = entity.ProductPicture;
            product.ProductStatus = entity.ProductStatus;
            product.ProductType = entity.ProductType;

            var save = _context.SaveChanges();

            ResultObj result = new ResultObj();
            if (save > 0)
            {

                result.Result = true;
                result.Message = "修改成功";
                result.Key = product.ProductId.ToString();
                return result;
            }
            else { result.Message = "修改失敗";  return result; }
        }

        public IQueryable<Product> Product()
        {
            return _context.Products;
        }

        public ResultObj RemoveProduct(int entity)
        {

            ResultObj result = new ResultObj();
            _context.Remove(Product().Where(x => x.ProductId == entity).FirstOrDefault());
            var save = _context.SaveChanges();

            if (save > 0)
            {
                result.Result = true;
                result.Message = "刪除成功";
                return result;
            }
            else
            {

                result.Message = "刪除失敗"; return result;
            }
            
        }
        public ResultObj CancelProduct(int entity)
        {
            var product = Product().Where(x => x.ProductId == entity).FirstOrDefault();
            product.ProductStatus = "不可點餐";
            var save = _context.SaveChanges();
            ResultObj result = new ResultObj();
            if (save > 0)
            {
                result.Key = entity.ToString();
                result.Result = true;
                result.Message = "成功";
                return result;
            }
            else
            {

                result.Message = "失敗"; return result;
            }
        }
    }
}
