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
using MyFinance.DocumentGeneration.Domain.Services;
using MyFinance.DocumentGeneration.Domain.Models;

namespace MyFinance.DocumentGeneration
{
    public class Function1
    {
        private readonly IReportGenerationService _reportGenerationService;

        public Function1(IReportGenerationService reportGenerationService)
        {
            _reportGenerationService = reportGenerationService;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var content = await new StreamReader(req.Body).ReadToEndAsync();
            StatisticsByDateReportModel parsedContent = JsonConvert.DeserializeObject<StatisticsByDateReportModel>(content);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var result = _reportGenerationService.GenerateReportByDateStats(parsedContent);

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new FileContentResult(result, "application/pdf");
        }
    }
}
