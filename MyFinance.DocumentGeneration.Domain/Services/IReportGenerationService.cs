using MyFinance.DocumentGeneration.Domain.Models;

namespace MyFinance.DocumentGeneration.Domain.Services
{
    public interface IReportGenerationService
    {
        byte[] GenerateReportByDateStats(StatisticsByDateReportModel statisticsByDateReportModel);
    }
}
