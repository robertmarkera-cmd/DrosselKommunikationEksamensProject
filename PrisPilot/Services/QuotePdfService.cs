using PrisPilot.Models;
using PrisPilot.Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.IO;

namespace PrisPilot.Services
{
    public class QuotePdfService
    {
        public Uri GeneratePreview(QuoteDraft draft)
        {
            CleanupOldPreviewFiles();

            string path = Path.Combine(
                Path.GetTempPath(),
                $"quote_preview_{DateTime.UtcNow.Ticks}.pdf");

            Generate(path, draft, null);

            return new Uri(path);
        }

        public void GenerateFinal(
            string path,
            QuoteDraft draft,
            Quote quote)
        {
            Generate(path, draft, quote);
        }

        private void CleanupOldPreviewFiles()
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(
                             Path.GetTempPath(),
                             "quote_preview_*.pdf"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // File may still be locked by WebView2, in that case we just ignore.
                        Debug.WriteLine("File is still locked; failed cleaning up file at " + DateTime.Now);
                    }
                }
            }
            catch
            {
                // In this case we ignore any cleanup failures.
                Debug.WriteLine("Failed cleaning up file at " + DateTime.Now);
            }
        }

        private void Generate(
            string path,
            QuoteDraft draft,
            Quote? quote)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Text("Drossel Kommunikation")
                            .FontSize(20).Bold();

                        row.ConstantItem(200).AlignRight().Column(col =>
                        {
                            col.Item().Text("Tilbud").FontSize(16).Bold();
                            if (quote != null)
                                col.Item().Text($"Nr.: {quote.QuoteID}");
                            col.Item().Text(DateTime.Now.ToShortDateString());
                        });
                    });

                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Item().Text("Til:").Bold();
                        col.Item().Text(draft.Customer?.CompanyName ?? "");

                        col.Item().PaddingTop(20).Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.ConstantColumn(100);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Text("Ydelse").Bold();
                                h.Cell().AlignRight().Text("Pris").Bold();
                            });

                            foreach (IProduct p in draft.Products)
                            {
                                table.Cell().Text(p.Name);
                                table.Cell().AlignRight()
                                    .Text($"{p.ProductPrice:n0} kr.");
                            }
                        });

                        col.Item().PaddingTop(20).AlignRight().Column(price =>
                        {
                            price.Item().Text($"Subtotal: {draft.Subtotal:n0} kr.");
                            price.Item().Text($"Samlet pris: {draft.Total:n0} kr.")
                                       .Bold().FontSize(14);
                        });
                    });

                    page.Footer().AlignCenter()
                        .Text("Gyldigt i 30 dage");
                });
            })
            .GeneratePdf(path);
        }
    }
}