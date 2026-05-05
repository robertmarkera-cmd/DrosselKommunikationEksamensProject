using PrisPilot.Models;
using PrisPilot.Services;
using PrisPilot.Services.Peristence;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace PrisPilot.ViewModels
{
    public class AddQuoteViewModel : SuperClassViewModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly QuoteRepository _quoteRepository;

        public ObservableCollection<Customer> Customers { get; set; }

        public QuoteDraft Draft { get; } = new();

        private Uri? _pdfPreviewUri;
        public Uri? PdfPreviewUri
        {
            get => _pdfPreviewUri;
            set
            {
                _pdfPreviewUri = value;
                OnPropertyChanged();
            }
        }

        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                Draft.Customer = value;
                OnPropertyChanged();
                RegeneratePreview();
            }
        }

        public AddQuoteViewModel()
        {
            _customerRepository = new CustomerRepository();
            _quoteRepository = new QuoteRepository();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(
                _customerRepository.GetAll());
        }

        private void RegeneratePreview()
        {
            Debug.WriteLine("REGENERATE PREVIEW CALLED " + DateTime.Now);
            Draft.Subtotal = Draft.FixedPriceProducts.Sum(p => p.Price);

            string path = Path.Combine(
                Path.GetTempPath(),
                "quote_preview.pdf");

            QuotePdfGenerator.GeneratePreview(path, Draft);
            PdfPreviewUri = new Uri(path);
        }

        public void SaveQuote()
        {
            var quote = new Quote
            {
                Date = DateTime.Now,
                TotalPrice = Draft.Total
            };

            quote = _quoteRepository.Add(quote);

            foreach (var product in Draft.FixedPriceProducts)
            {
                _quoteRepository.AddFixedPriceProductToQuote(
                    quote.QuoteID,
                    product.FixedPriceProductID);
            }

            var finalPath =
                $@"C:\Tilbud\Tilbud_{quote.QuoteID}.pdf";

            QuotePdfGenerator.GenerateFinal(finalPath, Draft, quote);
        }
    }
}