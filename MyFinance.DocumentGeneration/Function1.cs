using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using MyFinance.DocumentGeneration.Domain.Aspose;

namespace MyFinance.DocumentGeneration
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            Dictionary<string, string> parsedContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var res = AsposeLicenseService.Test(parsedContent, null);

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new FileContentResult(res, "application/pdf");
        }
    }
}
