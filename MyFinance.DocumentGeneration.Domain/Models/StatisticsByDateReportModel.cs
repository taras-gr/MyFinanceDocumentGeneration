namespace MyFinance.DocumentGeneration.Domain.Models
{
    public class StatisticsByDateReportModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Dictionary<DateTime, int> ExpensesByDate { get; set; }
    }
}
