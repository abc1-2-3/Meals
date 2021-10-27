using Meals.BLL.ImplementBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.InterfaceRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Repository.ImplementRepository
{
    public class BomRepository : BaseBLL,IBomRepository
    {
        private readonly DBContext _dBContext;

        public BomRepository(ILogger<BomRepository> logger, DBContext dBContext):base(logger)
        {
            _dBContext = dBContext;
        }

        public ResultObj CreateBom(List<Bom> entity)
        {
            ResultObj result = new ResultObj();

            foreach (var bom in entity)
            {

                if (_dBContext.Items.Where(x => x.ItemId == bom.ItemId).Count() > 0 && _dBContext.Products.Where(x => x.ProductId == bom.ProductId).Count() > 0)
                {
                    _dBContext.Add(bom);

                }
                else
                {
                    result.Message = "沒有物料或產品";
                    return result;
                }
            }
            _dBContext.SaveChanges();
            result.Result = true;
            result.Message = "建立成功";
            return result;
        }

        public ResultObj DeleteBom(int entity)
        {
            var bom = _dBContext.Boms.Where(x => x.ProductId == entity).ToList();
            foreach (var a in bom)
            {
                _dBContext.Remove(a);
            }

            var r = _dBContext.SaveChanges();
            ResultObj result = new ResultObj();
            if (r > 0)
            {
                result.Result = true;
                result.Message = "刪除成功";
            }
            else result.Message = "刪除失敗";
            return result;
        }

        public ResultObj ModifyBom(List<Bom> entity)
        {
            ResultObj result = new ResultObj();
            try
            {
               
                foreach (var bom in entity)
                {
                    var boms = _dBContext.Boms.Where(x => x.AutoId == bom.AutoId && x.ProductId == bom.ProductId).FirstOrDefault();
                    boms.ItemId = bom.ItemId;
                    boms.ItemUsageAmount = bom.ItemUsageAmount;
                    boms.ModifyDate = bom.ModifyDate;
                }

                var save = _dBContext.SaveChanges();
                if (save > 0)
                {
                    result.Result = true;
                    result.Message = "修改成功";
                }
                else result.Message = "修改失敗";

            }
            catch (Exception ex) {
                _logger.LogInformation(ex.ToString());

            }
            return result;
        }
    }
}
