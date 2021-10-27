using Meals.BLL.InterfaceBLL;
using Meals.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBLL _order;
        public OrderController(IOrderBLL order)
        {
            _order = order;
        }
        [HttpPost]
        public async Task<ResultObj> CreateOrder(Order2DTO entity)
        {
            return await Task.Run(() => _order.CreateOrder(entity));
        }
        [HttpPut]
        public async Task<ResultObj> ModifyOrderDetail(OrderDTO entity)
        {
            return await Task.Run(() => _order.ModifyOrderDetail(entity));
        }

        [HttpDelete]
        public async Task<ResultObj> ModifyStatusOrder(OrderSratusDTO entity)
        {
            return await Task.Run(() => _order.ModifyStatusOrder(entity));
        }
        [HttpPut]
        public async Task<ResultObj> CancelOrder(string orderId)
        {
            return await Task.Run(() => _order.CancelOrder(orderId));
        }
    }
}
