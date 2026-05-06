using PrisPilot.Models;
using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
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
        private readonly FixedPriceProductRepository _fixedRepo;
        private readonly VariablePriceProductRepository _variableRepo;

        public ObservableCollection<CustomerViewModel> CustomerVMCollection { get; set; }

        public ObservableCollection<IProduct> Products { get; }
        public ObservableCollection<IProduct> SelectedProducts { get; set; }

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
            _fixedRepo = new FixedPriceProductRepository();
            _variableRepo = new VariablePriceProductRepository();

            // Initialize CurrentQuote and CurrentTemplate
            CurrentQuote = new(new Quote());
            CurrentTemplate = new(new Template());

            // Initialize the ObservableCollection CustomerVMCollection
            CustomerVMCollection = new ObservableCollection<CustomerViewModel>();
            InitializeCustomerCollection();

            // Initialize the product vm
            Products = new ObservableCollection<IProduct>();
            SelectedProducts = new ObservableCollection<IProduct>();
            LoadProductCollections();
        }

        private void InitializeCustomerCollection()
        {
            foreach (Customer customer in _customerRepository.GetAll())
            {
                CustomerViewModel cw = new(customer);
                CustomerVMCollection.Add(cw);
            }
        }

        private void LoadProductCollections()
        {
            List<FixedPriceProduct> fixedItems = _fixedRepo.GetAll();
            List<VariablePriceProduct> variableItems = _variableRepo.GetAll();

            if (fixedItems.Count > 0)
            {
                foreach (FixedPriceProduct p in fixedItems)
                {
                    Products.Add(p);
                }
            }

            if (variableItems.Count > 0)
            {
                foreach (VariablePriceProduct p in variableItems)
                {
                    Products.Add(p);
                }

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