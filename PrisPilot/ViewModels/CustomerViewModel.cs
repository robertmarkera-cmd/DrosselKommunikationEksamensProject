using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrisPilot.ViewModels
{
    public class CustomerViewModel : BaseViewModel<Customer>
    {
        private Customer customer;

        private string _companyName = string.Empty;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (_companyName == value) return;
                _companyName = value;
                OnPropertyChanged();
            }
        }

        private int _cvr = 0;
        public int Cvr
        {
            get => _cvr;
            set
            {
                if (_cvr == value) return;
                _cvr = value;
                OnPropertyChanged();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        private int _telephoneNumber = 0;
        public int TelephoneNumber
        {
            get => _telephoneNumber;
            set
            {
                if (_telephoneNumber == value) return;
                _telephoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _address = string.Empty;
        public string Address
        {
            get => _address;
            set
            {
                if (_address == value) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        private Image _logo;
        public Image Logo
        {
            get => _logo;
            set
            {
                if (_logo == value) return;
                _logo = value;
                OnPropertyChanged();
            }
        }

        private string _contactPerson = string.Empty;
        public string ContactPerson
        {
            get => _contactPerson;
            set
            {
                if (_contactPerson == value) return;
                _contactPerson = value;
                OnPropertyChanged();
            }
        }

        private int _hourlyCost = 0;
        public int HourlyCost
        {
            get => _hourlyCost;
            set
            {
                if (_hourlyCost == value) return;
                _hourlyCost = value;
                OnPropertyChanged();
            }
        }

        public CustomerViewModel(Customer customer) : base(customer)
        {
            this.customer = customer;
            
        }
    }
}
