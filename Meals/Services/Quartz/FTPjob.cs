using FluentFTP;
using Meals.BLL.ImplementBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Services.Quartz
{
    [DisallowConcurrentExecution]
    public class FTPjob : IJob
    {
        private readonly ILogger<OrderCheckJob> _logger;
        private readonly DBContext _context;
        private readonly SerchBLL _serch;
        private readonly OrderBLL _order;
        private readonly IConfiguration Configuration;
        //private readonly DBContext _dBContext;
        public FTPjob(IConfiguration configuration ,OrderBLL order,ILogger<OrderCheckJob> logger,DBContext context,SerchBLL serch)
        {
            Configuration = configuration;
            _order = order;
            _serch = serch;
            _context = context;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            if (_context.Orders.Where(x => x.OrderStatus == "已結帳").Count() > 0)
            {
                try
                {
                    var ret = _serch.SerchOrderQuartz("已結帳");

                    List<FTPJobDTO> FTPlist = new List<FTPJobDTO>();

                    foreach (var id in ret)
                    {
                        FTPJobDTO order = new FTPJobDTO()
                        {
                            CustomerID="abc" + id.CustomerId,
                            OrderNo = id.OrderId,
                            TotalAmount =id.OrderPrice,
                            Status = "已結帳"
                        }; FTPlist.Add(order);
                    }

                    var host = Configuration.GetValue<string>("FTPConnect:host");
                    var pass = Configuration.GetValue<string>("FTPConnect:pass");
                    var user = Configuration.GetValue<string>("FTPConnect:user");
                    string json = JsonConvert.SerializeObject(FTPlist);
                    System.IO.File.WriteAllText(@"D:/order/out/已結帳.json", json);
                    FtpClient client = new FtpClient(host, 21, user, pass);
                    client.Connect();
                    if (client.IsConnected)
                    {
                        FileStream fileStream = new FileStream(@"D:/order/out/已結帳.json", FileMode.Open, FileAccess.Read);
                        client.Upload(fileStream, "已結帳Order.txt");
                        client.Disconnect();
                        
                    }
                    foreach (var id in FTPlist)
                    {
                        _order.FTPModifyStatusOrder(id.OrderNo);
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
