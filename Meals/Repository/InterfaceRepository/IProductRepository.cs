using Meals.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.InterfaceRepository
{
    public interface IProductRepository
    {
        public IQueryable<Product> Product();
        public ResultObj CreateProduct(Product entity);
        public ResultObj ModifyProduct(Product entity);
        public ResultObj RemoveProduct(int entity);
        public ResultObj CancelProduct(int entity);
    }
}
