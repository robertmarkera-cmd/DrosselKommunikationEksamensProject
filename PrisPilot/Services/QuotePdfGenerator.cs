using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PrisPilot.Models;

namespace PrisPilot.Services
{
    public static class QuotePdfGenerator
    {
        public static void GeneratePreview(string path, QuoteDraft draft)
        {
            Generate(path, draft, null);
        }

        public static void GenerateFinal(
            string path,
            QuoteDraft draft,
            Quote quote)
        {
            Generate(path, draft, quote);
        }

        private static void Generate(
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

                            foreach (var p in draft.FixedPriceProducts)
                            {
                                table.Cell().Text(p.Name);
                                table.Cell().AlignRight()
                                    .Text($"{p.Price:n0} kr.");
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