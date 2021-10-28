using Meals.BLL.ImplementBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Services.Quartz
{
    [DisallowConcurrentExecution]
    public class OrderCheckJob : IJob
    {
        private readonly ILogger<OrderCheckJob> _logger;
        private readonly DBContext _context;
        private readonly SerchBLL _serch;
        private readonly OrderBLL _order;
        public OrderCheckJob(OrderBLL order,ILogger<OrderCheckJob> logger,DBContext context,SerchBLL serch)
        {
            _order = order;
            _serch = serch;
            _context = context;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            if (_context.Orders.Where(x => x.OrderStatus == "成立").Count() > 0)
            {
                try
                {
                    var ret = _serch.SerchOrderQuartz();
                    string json = JsonConvert.SerializeObject(ret);
                    System.IO.File.WriteAllText(@"D:/order/out/order20211019.json", json);

                    foreach(var id in ret)
                    {
                        OrderSratusDTO order = new OrderSratusDTO()
                        {
                            OrderId = id.OrderId,
                            ModifyDate = DateTime.Now,
                            ProductId = id.ProductId,
                            Status = "處理中"
                        };
                        _order.ModifyStatusOrder(order);
                        
                    }


                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.StackTrace);
                }

            }

            return Task.CompletedTask;
        }
    }
}
