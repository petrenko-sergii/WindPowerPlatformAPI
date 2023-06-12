using Microsoft.AspNetCore.Mvc;

namespace WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces
{
    public interface IAzureResponseHelper
    {
        ActionResult<string> CheckFormattedDescFuncResponse(string formattedDescription);
    }
}
