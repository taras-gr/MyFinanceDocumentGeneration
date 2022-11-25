using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyFinance.DocumentGeneration.Domain.Services;

[assembly: FunctionsStartup(typeof(MyFinance.DocumentGeneration.Startup))]

namespace MyFinance.DocumentGeneration
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IReportGenerationService, ReportGenerationService>();
        }
    }
}
