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

        private bool _showLinkedInFastVaretagelse;
        public bool ShowLinkedInFastVaretagelse
        {
            get => _showLinkedInFastVaretagelse;
            set
            {
                if (_showLinkedInFastVaretagelse == value) return;
                _showLinkedInFastVaretagelse = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedProductTypesText));
            }
        }

        private bool _showFastvaretagelseKommunikation;
        public bool ShowFastvaretagelseKommunikation
        {
            get => _showFastvaretagelseKommunikation;
            set
            {
                if (_showFastvaretagelseKommunikation == value) return;
                _showFastvaretagelseKommunikation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedProductTypesText));
            }
        }

        private bool _showSalgsOgPraesentationMateriale;
        public bool ShowSalgsOgPraesentationMateriale
        {
            get => _showSalgsOgPraesentationMateriale;
            set
            {
                if (_showSalgsOgPraesentationMateriale == value) return;
                _showSalgsOgPraesentationMateriale = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedProductTypesText));
            }
        }

        private bool _showYderligereTilfoejelser;
        public bool ShowYderligereTilfoejelser
        {
            get => _showYderligereTilfoejelser;
            set
            {
                if (_showYderligereTilfoejelser == value) return;
                _showYderligereTilfoejelser = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedProductTypesText));
            }
        }

        public string SelectedProductTypesText
        {
            get
            {
                List<string> selected = new List<string>();

                if (ShowLinkedInFastVaretagelse) selected.Add("LinkedIn");
                if (ShowFastvaretagelseKommunikation) selected.Add("Kommunikation");
                if (ShowSalgsOgPraesentationMateriale) selected.Add("Salgs/præsentation");
                if (ShowYderligereTilfoejelser) selected.Add("Yderligere");

                return selected.Count == 0 ? "Vælg produkttyper..." : string.Join(", ", selected);
            }
        }

        public AddQuoteViewModel()
        {
            _customerRepository = new CustomerRepository();
            LoadCustomers();

            // Default: show nothing until the user chooses in the dropdown
            ShowLinkedInFastVaretagelse = false;
            ShowFastvaretagelseKommunikation = false;
            ShowSalgsOgPraesentationMateriale = false;
            ShowYderligereTilfoejelser = false;
        }

        private void LoadCustomers()
        {
            var customers = _customerRepository.GetAll();
            Customers = new ObservableCollection<Customer>(customers);
        }
    }
}
