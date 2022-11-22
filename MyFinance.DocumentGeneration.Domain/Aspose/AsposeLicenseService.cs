using Aspose.Words;
using Aspose.Words.Fields;
using Aspose.Words.Saving;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.DocumentGeneration.Domain.Aspose
{
    public class AsposeLicenseService
    {
        public static byte[] Test(Dictionary<string, string> statsByDate, Dictionary<string, string> statsByCategory)
        {
            // Create a new empty document A
            Document docA = new Document(Directory.GetCurrentDirectory() + "\\Templates\\Template.docx");

            // Inisialize a DocumentBuilder
            DocumentBuilder builder = new DocumentBuilder(docA);

            MemoryStream memoryStream= new MemoryStream();

            PdfSaveOptions pdfSaveOptions = new PdfSaveOptions()
            {
                TextCompression = PdfTextCompression.None,
                SaveFormat = SaveFormat.Pdf
            };

            builder.MoveToBookmark("content");

            //// Create column headers
            builder.StartTable();
            builder.InsertCell();
            builder.Write("День");
            builder.InsertCell();
            builder.Write("Сума витрат");
            builder.EndRow();

            foreach (var item in statsByDate)
            {
                builder.InsertCell();
                builder.Write(item.Key);
                builder.InsertCell();
                builder.Write(item.Value);
                builder.EndRow();
            }

            //// We have a second data table called "Orders", which has a many-to-one relationship with "Customers"
            //// picking up rows with the same CustomerID value.
            //builder.InsertCell();
            //builder.InsertField(" MERGEFIELD TableStart:Orders");
            //builder.InsertField(" MERGEFIELD ItemName");
            //builder.InsertCell();
            //builder.InsertField(" MERGEFIELD Quantity");
            //builder.InsertField(" MERGEFIELD TableEnd:Orders");
            //builder.EndTable();

            //// The end point of mail merge with regions.
            //builder.InsertField(" MERGEFIELD TableEnd:Customers");

            // Save the output as PDF
            docA.Save(memoryStream, pdfSaveOptions);

            return memoryStream.ToArray();
        }
    }
}
