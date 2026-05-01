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

        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
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
                OnPropertyChanged();
            }
        }

        public AddQuoteViewModel()
        {
            _customerRepository = new CustomerRepository();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var customers = _customerRepository.GetAll();
            Customers = new ObservableCollection<Customer>(customers);
        }
    }
}
