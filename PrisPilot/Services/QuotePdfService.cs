using PrisPilot.Models;
using PrisPilot.Services.Interfaces;
using PrisPilot.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows;

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
                        Debug.WriteLine("File is still locked");
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Cleanup failed");
            }
        }

        private void Generate(
            string path,
            QuoteDraft draft,
            Quote? quote)
        {
            string tempDocPath = Path.GetTempFileName();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);

                    page.Margin(40);

                    page.PageColor("#fbf8f2");

                    page.DefaultTextStyle(x =>
                        x.FontSize(11)
                         .FontColor("#172e79"));

                    // FOOTER
                    BuildFooter(page);

                    // FRONT PAGE
                    BuildFrontPage(page, draft, quote);
                });
            })
            .GeneratePdf(tempDocPath);

            // ABOUT US PAGE MERGE
            if (draft.IncludeAboutUs)
            {
                string aboutPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Assets",
                    "drossel_about_us_page.pdf");

                if (File.Exists(aboutPath))
                {
                    DocumentOperation
                        .LoadFile(tempDocPath)
                        .MergeFile(aboutPath)
                        .Save(path);

                    File.Delete(tempDocPath);

                    return;
                }
                else
                {
                    MessageBox.Show($"{aboutPath} blev ikke fundet");
                }
            }

            File.Move(tempDocPath, path, true);
        }

        // =========================================================
        // FRONT PAGE
        // =========================================================

        private void BuildFrontPage(
            PageDescriptor page,
            QuoteDraft draft,
            Quote? quote)
        {
            page.Content().Column(col =>
            {
                // TOP SPACING
                col.Item().Height(50);

                // LOGO / COMPANY NAME
                col.Item()
                .Width(320)
                .Image(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Assets",
                    "Logo",
                    "Drossel_Logo.png"));

                // TITLE
                col.Item().PaddingTop(40)
                    .Text("Tilbud På")
                    .FontSize(32)
                    .FontColor("#172e79")
                    .SemiBold();

                //col.Item().PaddingTop(5)
                //    .Text(string.Join(", ", draft.Products.Select(p => p.Product.Name)))
                //    .FontSize(18);

                // DATE
                col.Item().PaddingTop(20)
                    .Text(DateTime.Now.ToShortDateString());

                // BIG SPACING
                col.Item().Height(80);

                // CONTACT + CUSTOMER LOGO
                col.Item().Row(row =>
                {
                    // LEFT SIDE
                    row.RelativeItem().Column(left =>
                    {
                        left.Item().Text("Kontaktoplysninger")
                            .Bold();

                        left.Item().PaddingTop(10)
                            .Text("Drossel Kommunikation");

                        left.Item().Text("info@drosselkommunikation.dk");

                        left.Item().Text("Tlf. 28 45 14 48");
                    });

                    // RIGHT SIDE
                    row.ConstantItem(180)
                    .Height(120)
                         .AlignCenter()
                        .AlignMiddle()
                        .Text("Kundelogo");
                });

                // SPACE
                col.Item().Height(120);

                // CUSTOMER
                BuildCustomerSection(col, draft);

                // SPACE
                col.Item().Height(40);

                // INTRODUCTION
                BuildIntroduktionSection(col, draft);

                // SPACE
                col.Item().Height(90);


                IEnumerable<ProductViewModel> services = draft.Products.Where(p => p.IsSelected);

                foreach (ProductViewModel service in services)
                {
                    col.Item().PageBreak();
                    col.Item().Text(service.Product.Name).FontSize(20).Bold();
                    col.Item().PaddingTop(10).Text(service.Product.Description);
                }

                //if (services.Any())
                //{
                //    col.Item().PageBreak();
                //}
                // PRICE TABLE
                BuildPriceTable(col, draft);

               
                
            });
        }
        // =========================================================
        // INTRODUKTION SECTION 
        // =========================================================
        private void BuildIntroduktionSection(ColumnDescriptor col, QuoteDraft draft)
        {
            col.Item().PageBreak();
            col.Item().Text("Indledning")
                .FontSize(18)
                .Bold();

            col.Item().PaddingTop(10)
                .Text(draft.Introduction ?? string.Empty);
        }
        // =========================================================
        // CUSTOMER SECTION
        // =========================================================

        private void BuildCustomerSection(
            ColumnDescriptor col,
            QuoteDraft draft)
        {
            col.Item().Text("Til:")
                .FontSize(16)
                .Bold();

            col.Item().PaddingTop(10)
                .Text(draft.Customer?.CompanyName ?? "");
        }

        // =========================================================
        // PRICE TABLE
        // =========================================================

        private void BuildPriceTable(
            ColumnDescriptor col,
            QuoteDraft draft)
        {
            col.Item().PageBreak();
            col.Item().Text("Pris")
                .FontSize(18)
                .Bold();

            col.Item().PaddingTop(15).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.ConstantColumn(120);
                });

                // HEADER
                table.Header(header =>
                {
                    header.Cell().PaddingBottom(15)
                    .Text("Ydelse")
                        .Bold();

                    header.Cell()
                        .PaddingBottom(10)
                        .AlignRight()
                        .Text("Pris")
                        .Bold();
                });

                col.Item()
                    .PaddingTop(30)
                    .AlignRight()
                    .Column(price =>
                    {
                        price.Item()
                            .Text($"Subtotal: {draft.Subtotal:n0} kr.");

                        price.Item()
                            .PaddingTop(10)
                            .Text($"Samlet pris: {draft.Total:n0} kr.")
                            .FontSize(24)
                            .FontColor("#172e79")
                            .Bold();
                    });
                // PRODUCTS
                foreach (ProductViewModel pvm in draft.Products)
                {
                    IProduct product = pvm.Product;

                    table.Cell()
                        .PaddingVertical(14)
                        .Text(product.Name);

                    table.Cell()
                        .PaddingVertical(8)
                        .AlignRight()
                        .Text($"{product.ProductPrice:n0} kr.");
                }
            });
        }



        // =========================================================
        // FOOTER
        // =========================================================

        private void BuildFooter(PageDescriptor page)
        {
            page.Footer()
                .PaddingBottom(10)
                .AlignCenter()
                .Text(text =>
                {
                    text.DefaultTextStyle(x =>
                        x.FontSize(10)
                         .FontColor("#172e79"));

                    text.Span("info@drosselkommunikation.dk");
                    text.Span("  •  ");
                    text.Span("Tlf. 28 45 14 48");
                    text.Span("  •  ");
                    text.Span("www.drosselkommunikation.dk");
                });
        }
    }
}



