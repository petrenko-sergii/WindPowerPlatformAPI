using Microsoft.AspNetCore.Mvc;
using WindPowerPlatformAPI.Infrastructure.Filters;

namespace WindPowerPlatformAPI.Infrastructure.Attributes
{
    public class UniqueSerialNumberAttribute : TypeFilterAttribute
    {
        public UniqueSerialNumberAttribute() : base(typeof(UniqueSerialNumberFilter))
        {
            
        }
    }
}
