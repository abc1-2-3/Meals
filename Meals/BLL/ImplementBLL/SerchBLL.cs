using Meals.BLL.InterfaceBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Meals.BLL.ImplementBLL
{
    public class SerchBLL: BaseBLL,ISerchBLL
    {
        private readonly DBContext _context;
        public SerchBLL(ILogger<SerchBLL> logger, DBContext context) : base(logger)
        {
            _context = context;

        }

        public PageOrderDTO SerchOrder(string status, int? page)
        {
            PageOrderDTO pagedto = new PageOrderDTO();
            try
            {
                var order = _context.Orders.Select(x=>x);
                if (status != null) order = order.Where(x => x.OrderStatus == status);
                
                order=  from o in order
                        orderby o.CreateDate descending
                        select o;
                List<SerchOrderDTO> serchOrder = new List<SerchOrderDTO>(order.Select(x => new SerchOrderDTO()
                {
                    CreateDate = x.CreateDate,
                    CustomerId = x.CustomerId,
                    OrderPrice = x.OrderPrice,
                    OrderId = x.OrderId,
                    OrderStatus = x.OrderStatus,
                    OrderSubject = x.OrderSubject,
                    TableNumber = x.TableNumber
                }));

                var ret = serchOrder.ToPagedList(page ?? 1, 10);

                pagedto.list = ret;
                pagedto.PageCount = ret.PageCount;

                return pagedto;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
            }
            return null;
        }
        public PageProductlDTO SerchProduct(string Type, int? page)
        {
            PageProductlDTO pagedto = new PageProductlDTO();
            try
            {
                var Products = _context.Products.Select(x => x);
                if (Type != null) Products = Products.Where(x => x.ProductType == Type);

                var producctdto=Products.Select(x => new ProductDTO()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    ProductStatus = x.ProductStatus,
                    ProductPicture = x.ProductPicture,
                    ProductType = x.ProductType

                });
                var ret = producctdto.ToPagedList(page ?? 1, 6);

                pagedto.list = ret;
                pagedto.PageCount = ret.PageCount;

                return pagedto;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
            }
            return null;
        }

    }
}
