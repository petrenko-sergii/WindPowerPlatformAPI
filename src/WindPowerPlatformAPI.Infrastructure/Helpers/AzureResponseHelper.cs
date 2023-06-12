using Microsoft.AspNetCore.Mvc;
using System.Net;
using WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Helpers
{
    public class AzureResponseHelper : ControllerBase, IAzureResponseHelper
    {
        public ActionResult<string> CheckFormattedDescFuncResponse(string formattedDescription)
        {
            var funcApp = "Azure Function App";

            if (string.IsNullOrEmpty(formattedDescription))
                return NotFound();
            else if (formattedDescription == HttpStatusCode.NotFound.ToString())
                return BadRequest($"{funcApp} \"TurbineDescFormatter\" is turned off or broken");
            else if (formattedDescription == HttpStatusCode.BadRequest.ToString())
                return BadRequest($"Error happened during usage of {funcApp} \"TurbineDescFormatter\"");
            else if (formattedDescription.Contains("Error"))
                return BadRequest($"{formattedDescription} -- happened during usage of {funcApp} \"TurbineDescFormatter\".");

            return Ok(formattedDescription);
        }
    }
}
