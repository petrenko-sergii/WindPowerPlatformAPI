using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Exceptions;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Filters
{
    public class UniqueSerialNumberFilter : IActionFilter
    {
        private readonly ITurbineService _turbineService;

        public UniqueSerialNumberFilter(ITurbineService turbineService)
        {
            _turbineService = turbineService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var createDto = (TurbineCreateDto)context.ActionArguments.First().Value;
            var  serialNumber = createDto.SerialNumber;


            var existedSerialNumber = _turbineService.GetAllTurbines(string.Empty).FirstOrDefault(t => t.SerialNumber == serialNumber);

            if (existedSerialNumber != null)
            {
                throw new UniqueSerialNumberException($"Serial Number \"{serialNumber}\" already exists.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
