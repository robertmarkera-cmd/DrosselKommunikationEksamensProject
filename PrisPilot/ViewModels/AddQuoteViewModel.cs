using PrisPilot.Models;
using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
using PrisPilot.Services.Peristence;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                
                // Update recent cost
                _currentQuote.RecentCost = _quoteRepository.GetRecentHourlyCostForCustomer(_selectedCustomer.Cvr);

                OnPropertyChanged();
                RegeneratePreview();
            }
        }

        public string SelectedProductTypesText
        {
            get
            {
                List<string> selected = new List<string>();

                foreach (ProductViewModel p in SelectedProducts)
                {
                    selected.Add(p.Name);
                }

                return selected.Count == 0 ? "Vælg produkttyper..." : string.Join(", ", selected);
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

            // Didn't manage to find a good solution to merge these two lists, so we just run them one at a time
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
                    
                    // Subscribe to listen for HoursUsed changes
                    vm.PropertyChanged += Product_PropertyChanged;
                    
                    // Optionally regenerate preview when a new product is added
                    RegeneratePreview(); 
                }
                else if (!vm.IsSelected && SelectedProducts.Contains(vm))
                {
                    SelectedProducts.Remove(vm);
                    
                    // Unsubscribe to prevent unwanted triggers
                    vm.PropertyChanged -= Product_PropertyChanged;
                    
                    // Optionally regenerate preview when a product is removed
                    RegeneratePreview();
                }
            }
        }

        // Method for 

        private void Product_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Only trigger the regeneration when the specific property changes
            if (e.PropertyName == nameof(ProductViewModel.HoursUsed))
            {
                RegeneratePreview();
            }
        }

        private void RegeneratePreview()
        {
            // Debug
            Debug.WriteLine("REGENERATE PREVIEW CALLED " + DateTime.Now);

            QuoteDraft draft = CreateCurrentDraft();
            PdfPreviewUri = _quotePdfService.GeneratePreview(draft);
        }

        private QuoteDraft CreateCurrentDraft()
        {
            return new QuoteDraft
            {
                Customer = SelectedCustomer?.ToModel(),
                Products = SelectedProducts, // This is possible as Products is an IEnumerable, so we don't need to create a copy
            };
        }

        public void SaveQuote()
        {
            QuoteDraft draft = CreateCurrentDraft();

            CurrentQuote.TotalPrice = draft.Total;
            CurrentQuote.Date = DateTime.Now;

            // This gets the Quote model from our CurrentQuote ViewModel
            Quote quote = CurrentQuote.ToModel();

            // Here we are saving to the repository and retrieving the assigned QuoteID
            quote = _quoteRepository.Add(quote);

            // Here we are updating the CurrentQuote with our freshly generated ID
            CurrentQuote.QuoteID = quote.QuoteID;

            foreach (IProduct product in draft.Products)
            {
                _quoteRepository.AddFixedPriceProductToQuote(
                    quote.QuoteID,
                    product.Id);
            }

            string finalPath = $@"C:\Tilbud\Tilbud_{quote.QuoteID}.pdf";
            _quotePdfService.GenerateFinal(finalPath, draft, quote);
        }
    }
}