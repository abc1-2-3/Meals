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
        public ResultObj CreateOrder(Order2DTO entity)
        {
            return _order.CreateOrder(entity);
        }
        [HttpPut]
        public ResultObj ModifyOrderDetail(OrderDTO entity)
        {
            return _order.ModifyOrderDetail(entity);
        }

        [HttpDelete]
        public ResultObj ModifyStatusOrder(OrderSratusDTO entity)
        {
            return _order.ModifyStatusOrder(entity);
        }
        [HttpPut]
        public ResultObj CancelOrder(string orderId)
        {
            return _order.CancelOrder(orderId);
        }
    }
}
