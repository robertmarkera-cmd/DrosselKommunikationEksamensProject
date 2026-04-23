using PrisPilot.Models;
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
            this._customer = customer;
            this.CompanyName = customer.CompanyName;
            this.Cvr = customer.Cvr;
            this.Email = customer.Email;
            this.TelephoneNumber = customer.TelephoneNumber;
            this.Address = customer.Address;
            this.Logo = customer.Logo;
            this.ContactPerson = customer.ContactPerson;
            this.HourlyCost = customer.HourlyCost;
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
            try
            {
                using MemoryStream ms = new MemoryStream(_logo);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();
                PreviewImage = bitmap;
            }
            catch
            {
                PreviewImage = null;
            }
        }
    }
}
