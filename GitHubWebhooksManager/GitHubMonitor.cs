using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GitHubWebhooksManager.Models;

namespace GitHubWebhooksManager
{
    public static class GitHubMonitor
    {
        [FunctionName("GitHubMonitor")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation("Our GitHub Monitor processed an action.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Rootobject>(requestBody);

            // Do something with data
            log.LogInformation(requestBody);

            return new OkResult();
        }
    }
}
