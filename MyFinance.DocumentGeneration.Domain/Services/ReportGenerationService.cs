using Aspose.Words.Saving;
using Aspose.Words;
using MyFinance.DocumentGeneration.Domain.Models;
namespace MyFinance.DocumentGeneration.Domain.Services
{
    public class ReportGenerationService : IReportGenerationService
    {
        public byte[] GenerateReportByDateStats(StatisticsByDateReportModel statisticsByDateReportModel)
        {
            // Create a new empty document A
            Document docA = new Document(Directory.GetCurrentDirectory() + "\\Templates\\Template.docx");

            // Inisialize a DocumentBuilder
            DocumentBuilder builder = new DocumentBuilder(docA);

            MemoryStream memoryStream = new MemoryStream();

            PdfSaveOptions pdfSaveOptions = new PdfSaveOptions()
            {
                TextCompression = PdfTextCompression.None,
                SaveFormat = SaveFormat.Pdf
            };

            docA.Range.Replace("<<TimePeriod>>", statisticsByDateReportModel.StartDate.ToString("dd-MM-yyyy") + " - " + statisticsByDateReportModel.EndDate.ToString("dd-MM-yyyy"));
            docA.Range.Replace("<<DocumentGenerationDate>>", DateTime.Now.ToString("dd-MM-yyyy-HH-m"));

            builder.MoveToBookmark("content");

            //// Create column headers
            builder.StartTable();
            builder.InsertCell();
            builder.Write("День");
            builder.InsertCell();
            builder.Write("Сума витрат");
            builder.EndRow();

            foreach (var item in statisticsByDateReportModel.ExpensesByDate)
            {
                builder.InsertCell();
                builder.Write(item.Key.ToString("dd.MM") + " (" + item.Key.ToString("dddd") + ")");
                builder.InsertCell();
                builder.Write(item.Value.ToString());
                builder.EndRow();
            }

            docA.Save(memoryStream, pdfSaveOptions);

            return memoryStream.ToArray();
        }
    }
}
