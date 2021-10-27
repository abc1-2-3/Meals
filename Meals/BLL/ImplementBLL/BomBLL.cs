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
    public class BomBLL:BaseBLL, IBomBLL
    {
        private readonly IBomRepository _bom;
        private readonly DBContext _context;

        public BomBLL(ILogger<BomBLL> logger, IBomRepository bom,DBContext context) : base(logger)
        {
            _bom = bom;
            _context = context;
        }

        public ResultObj CreateBom(List<BomDTO> entity)
        {
            ResultObj result = new ResultObj();
            List<Bom> boms = new List<Bom>();
            foreach (var bom in entity)
            {
                Bom b = new Bom()
                {
                    CreateDate = DateTime.Now,
                    ItemId=bom.ItemId,
                    ItemUsageAmount=bom.ItemUsageAmount,
                    ProductId=bom.ProductId
                    
                }; boms.Add(b);
            }
            result = _bom.CreateBom(boms);

            _logger.LogDebug("OK");
            return result;
        }

        public ResultObj DeleteBom(int entity)
        {
            ResultObj result = new ResultObj();
            result = _bom.DeleteBom(entity);
            _logger.LogDebug("OK");
            return result;
        }

        public ResultObj ModifyBom(List<BomDTO> entity)
        {
            ResultObj result = new ResultObj();
            try
            {
                List<Bom> boms = new List<Bom>();
                foreach (var bom in entity)
                {
                    if ((_context.Boms.Where(x => x.AutoId == bom.AutoId).Count() < 0))
                    {
                        result.Message = "無BOM資料";
                        return result;
                    }
                    else if ((_context.Items.Where(x => x.ItemId == bom.ItemId).Count() < 0))
                    {
                        result.Message = "無物料";
                        return result;
                    }
                    else if ((_context.Products.Where(x => x.ProductId == bom.ProductId).Count() < 0))
                    {
                        result.Message = "無產品";
                        return result;
                    }

                    Bom b = new Bom()
                    {
                        AutoId = bom.AutoId,
                        CreateDate = DateTime.Now,
                        ItemId = bom.ItemId,
                        ItemUsageAmount = bom.ItemUsageAmount,
                        ProductId = bom.ProductId
                    }; boms.Add(b);
                }
                result = _bom.ModifyBom(boms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return result;
        }
    }
}
