using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.BLL.ImplementBLL
{
    public class BaseBLL
    {
        public readonly ILogger<BaseBLL> _logger;

        public BaseBLL(ILogger<BaseBLL> logger)
        {
            _logger = logger;
        }




    }
}
