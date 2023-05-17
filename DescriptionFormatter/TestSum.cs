using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DescriptionFormatter
{
    public static class TestSum
    {
        [FunctionName("TestSum")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int x = int.Parse(req.Query["x"]);
            int y = int.Parse(req.Query["y"]);

            int result = x + y;

            return new OkObjectResult(result);
        }
    }
}
