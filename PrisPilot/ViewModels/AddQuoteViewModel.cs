using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PrisPilot.Models;
using PrisPilot.Services.Peristence;

namespace PrisPilot.ViewModels
{
    public class AddQuoteViewModel : SuperClassViewModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly QuoteRepository _quoteRepository;
        private readonly TemplateRepository _templateRepository;

        public ObservableCollection<CustomerViewModel> CustomerVMCollection { get; set; }
        

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
                OnPropertyChanged();
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
    }
}
