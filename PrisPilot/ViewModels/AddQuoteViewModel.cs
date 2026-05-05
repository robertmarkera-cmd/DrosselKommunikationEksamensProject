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
        private readonly TemplateRepository _templateRepository;

        public ObservableCollection<CustomerViewModel> CustomerVMCollection { get; set; }

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

        private QuoteViewModel _currentQuote;
        public QuoteViewModel CurrentQuote
        {
            get => _currentQuote;
            set
            {
                _currentQuote = value;
                OnPropertyChanged();
            }
        }

        private TemplateViewModel _currentTemplate;
        public TemplateViewModel CurrentTemplate
        {
            get => _currentTemplate;
            set
            {
                _currentTemplate = value;
                OnPropertyChanged();
            }
        }

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer == value) return;
                _selectedCustomer = value;
                _currentQuote.Cvr = _selectedCustomer.Cvr;
                Draft.Customer = value?.ToModel();
                OnPropertyChanged();
                RegeneratePreview();
            }
        }

        public AddQuoteViewModel()
        {
            _customerRepository = new CustomerRepository();
            _quoteRepository = new QuoteRepository();
            _templateRepository = new TemplateRepository();

            // Initialize CurrentQuote and CurrentTemplate
            CurrentQuote = new(new Quote());
            CurrentTemplate = new(new Template());

            // Initialize the ObservableCollection CustomerVMCollection
            CustomerVMCollection = new ObservableCollection<CustomerViewModel>();
            InitializeCustomerCollection();
        }

        private void InitializeCustomerCollection()
        {
            foreach (Customer customer in _customerRepository.GetAll())
            {
                CustomerViewModel cw = new(customer);
                CustomerVMCollection.Add(cw);
            }
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