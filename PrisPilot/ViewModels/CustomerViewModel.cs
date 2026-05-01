using PrisPilot.Models;
using PrisPilot.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace PrisPilot.ViewModels
{
    public class CustomerViewModel : BaseViewModel<Customer>
    {

        private ImageService _imageService;
        private Customer _customer;

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

        private string _cvr = string.Empty;
        public string Cvr
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

        private string _telephoneNumber = string.Empty;
        public string TelephoneNumber
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

        private byte[]? _logo;
        public byte[]? Logo
        {
            get => _logo;
            set
            {
                if (_logo == value) return;
                _logo = value;
                OnPropertyChanged();
                UpdatePreviewFromLogo();
            }
        }

        // This is for our WPF preview
        // This is placed inside this class to help us access it from anywhere we're using the CustomerViewModel
        private BitmapImage? _previewImage;
        public BitmapImage? PreviewImage
        {
            get => _previewImage;
            private set
            {
                if (_previewImage == value) return;
                _previewImage = value;
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

        public CustomerViewModel(Customer customer) : base(customer)
        {
            this._customer = customer;
            this.CompanyName = customer.CompanyName;
            this.Cvr = customer.Cvr;
            this.Email = customer.Email;
            this.TelephoneNumber = customer.TelephoneNumber;
            this.Address = customer.Address;
            this.Logo = customer.Logo;
            this.ContactPerson = customer.ContactPerson;
        }


        public bool IsCustomerValid()
        {
            bool result = true;

            // CompanyName
            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                result = false;
            }

            // CVR
            if (Cvr.Length == 8)
            {
                int tempCvrInt;
                bool cvrIsNumber = int.TryParse(Cvr, out tempCvrInt);
                if (cvrIsNumber == false) // Alternate: if (!cvrIsNumber)
                {
                    result = false;
                }
            }
            else result = false;

            // Email
            if (string.IsNullOrWhiteSpace(Email))
            {
                result = false;
            }

            // ContactPerson
            if (string.IsNullOrWhiteSpace(ContactPerson))
            {
                result = false;
            }

            return result;
        }

        private void UpdatePreviewFromLogo()
        {
            // Check if logo exists, returns null if it doesn't
            if (_logo == null)
            {
                PreviewImage = null;
                return;
            }

            // Creating a BitmapImage from our bytearray
            _imageService = new();
            PreviewImage = _imageService.ReencodeToBitmap(_logo);
        }
    }
}
