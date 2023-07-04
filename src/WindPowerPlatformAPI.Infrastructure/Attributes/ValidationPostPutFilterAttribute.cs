using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.Infrastructure.Attributes
{
    public class ValidationPostPutFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is CommandBaseDto);

            if(param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Input param is null.");
                return;
            }

            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
