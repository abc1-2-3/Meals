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

        public PageOrderDTO SerchOrder( int? page, string status = null)
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
        public PageProductlDTO SerchProduct(int? page,string Type= null)
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

        public List<SerchOrder2DTO> SerchOrderQuartz(string Type = null)
        {
            try
            {
                var order = _context.Orders.Select(x => x);
                if (Type != null) order = order.Where(x => x.OrderStatus == Type);
                var serchOrder = from o in order
                                 join d in _context.OrderDetails on o.OrderId equals d.OrderId
                              join p in _context.Products on d.ProductId equals p.ProductId
                              select new SerchOrder2DTO()
                              {
                                  ProductId=p.ProductId,
                                  CreateDate = o.CreateDate,
                                  CustomerId = o.CustomerId,
                                  OrderPrice = o.OrderPrice,
                                  OrderId = o.OrderId,
                                  OrderStatus = o.OrderStatus,
                                  OrderSubject = o.OrderSubject,
                                  TableNumber = o.TableNumber
                              };


                return serchOrder.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
            }
            return null;
        }
    }
}
