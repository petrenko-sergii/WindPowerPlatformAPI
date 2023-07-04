using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WindPowerPlatformAPI.Infrastructure.Exceptions;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Attributes
{
    public class CommandExistsValidationAttribute<T> : IActionFilter where T : class
    {
        private readonly ICommandService _service;

        public CommandExistsValidationAttribute(ICommandService service)
        {
            _service = service;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id;

            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult($"Bad \"id\" parameter.");
                return;
            }

            var commandFromRepo = _service.GetCommandById(id);

            if (commandFromRepo == null)
            {
                throw new NotFoundException($"Command with id {id} is not found.");
            }
            else
            {
                context.HttpContext.Items.Add("command", commandFromRepo);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
