using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DescriptionFormatter
{
    public static class TurbineDescFormatter
    {
        [FunctionName("TurbineDescFormatter")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Function App \"TurbineDescFormatter\" was triggered by HTTP request.");

            string serialNumber = req.Query["serialNumber"];

            if (serialNumber.Length > 250)
            {
                var errorMsg = "Parameter \"SerialNumber\" must not exceed 250 characters.";
                log.LogWarning(errorMsg);
                return new BadRequestObjectResult(errorMsg);
            }

            decimal price;
            if (decimal.TryParse(req.Query["price"], out price))
            {
                string description = $"Turbine: serial number \"{serialNumber}\" has price {price} Euro.";

                log.LogInformation(description);
                return new OkObjectResult(description);
            }
            else
            {
                var errorMsg = "Error during formatting parameter \"Price\". Check it.";
                log.LogWarning(errorMsg);
                return new BadRequestObjectResult(errorMsg);
            }
        }
    }
}
