using PrisPilot.Models;
using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
using PrisPilot.Services.Peristence;
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
        private readonly TimeSpentRepository _timeSpentRepo;

        private readonly QuotePdfService _quotePdfService;

        public ObservableCollection<CustomerViewModel> CustomerVMCollection { get; set; }

        public ObservableCollection<ProductViewModel> Products { get; }
        public ObservableCollection<ProductViewModel> SelectedProducts { get; set; }

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
                // Update recent cost
                _currentQuote.RecentCost = _quoteRepository.GetRecentHourlyCostForCustomer(_selectedCustomer.Cvr);

                OnPropertyChanged();
                RegeneratePreview();
            }
        }

        public AddQuoteViewModel()
        {
            // Initialize repositories
            _customerRepository = new CustomerRepository();
            _quoteRepository = new QuoteRepository();
            _templateRepository = new TemplateRepository();
            _fixedRepo = new FixedPriceProductRepository();
            _variableRepo = new VariablePriceProductRepository();
            _timeSpentRepo = new TimeSpentRepository();

            // Initialize the pdf service
            _quotePdfService = new QuotePdfService();

            // Initialize CurrentQuote and CurrentTemplate
            CurrentQuote = new(new Quote());
            CurrentTemplate = new(new Template());

            // Initialize the ObservableCollection CustomerVMCollection
            CustomerVMCollection = new ObservableCollection<CustomerViewModel>();
            InitializeCustomerCollection();

            // Initialize the product vm
            Products = new ObservableCollection<ProductViewModel>();
            SelectedProducts = new ObservableCollection<ProductViewModel>();
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

            foreach (FixedPriceProduct? p in fixedItems)
            {
                ProductViewModel vm = new ProductViewModel(p);

                // Subscribe Product_IsSelectedChanged to the IsSelectedChanged event of the vm object
                vm.IsSelectedChanged += Product_IsSelectedChanged;
                
                Products.Add(vm);
            }

            foreach (VariablePriceProduct? p in variableItems)
            {
                ProductViewModel vm = new ProductViewModel(p);

                // Subscribe Product_IsSelectedChanged to the IsSelectedChanged event of the vm object
                vm.IsSelectedChanged += Product_IsSelectedChanged;

                Products.Add(vm);
            }
        }

        private void Product_IsSelectedChanged(object? sender, EventArgs e)
        {
            if (sender is ProductViewModel vm)
            {
                if (vm.IsSelected && !SelectedProducts.Contains(vm))
                {
                    // Add in the order they are checked
                    SelectedProducts.Add(vm);
                }
                else if (!vm.IsSelected && SelectedProducts.Contains(vm))
                {
                    SelectedProducts.Remove(vm);
                }
            }
        }

        private void RegeneratePreview()
        {
            // Debug
            Debug.WriteLine("REGENERATE PREVIEW CALLED " + DateTime.Now);

            //Draft.Subtotal = Draft.Products.Sum(p => p.Price);
            PdfPreviewUri = _quotePdfService.GeneratePreview(Draft);
        }

        public void SaveQuote()
        {
            Quote? quote = new Quote
            {
                Date = DateTime.Now,
                TotalPrice = Draft.Total
            };

            quote = _quoteRepository.Add(quote);

            foreach (IProduct product in Draft.Products)
            {
                _quoteRepository.AddFixedPriceProductToQuote(
                    quote.QuoteID,
                    product.Id);
            }

            string finalPath =
                $@"C:\Tilbud\Tilbud_{quote.QuoteID}.pdf";

            _quotePdfService.GenerateFinal(finalPath, Draft, quote);
        }
    }
}