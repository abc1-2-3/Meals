using Meals.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.InterfaceBLL
{
    public interface IProductBLL
    {
        public ResultObj CreateProduct(ProductDTO entity);
        public ResultObj ModifyProduct(ProductDTO entity);
        public ResultObj RemoveProduct(int entity);
        public ResultObj CancelProduct(int entity);
    }
}
