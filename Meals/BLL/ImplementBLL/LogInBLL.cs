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
    public class LogInBLL : BaseBLL,ILogInBLL
    {
        private readonly MD5 _md5;
        private readonly DBContext _dBContext;
        private readonly Jwt _jwt;

        public LogInBLL(Jwt jwt,ILogger<LogInBLL> logger, MD5 d5, DBContext dBContext) : base(logger)
        {
            _md5 = d5;
            _dBContext = dBContext;
            _jwt = jwt;
        }
        public ResultObj LogIn(LogInDTO entity)
        {
            ResultObj result = new ResultObj();
            try
            {
                string MD5password = _md5.getMd5Method2(entity.CustomerPassword);
                var user = _dBContext.Customers.Where(x => x.CustomerAccount == entity.CustomerAccount && x.CustomerPassword == MD5password).FirstOrDefault();
                if (user != null)
                {
                    result.Result = true;
                    string serializeToken = _jwt.GenerateToken(user.CustomerAccount);
                    result.Message = serializeToken;
                    result.Key = user.CustomerId.ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                result.Message = "帳號或密碼錯誤";
            }
            return result;
        }
    }
}
