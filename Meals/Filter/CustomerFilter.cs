using Meals.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meals.Filter
{
    public class CustomerFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var param = context.ActionArguments["entity"] as CustomerDTO;


            if (param.CustomerId == 0)
            {
                context.Result = new BadRequestObjectResult("欄位必填");
                return;
            }
        }
    }
}
