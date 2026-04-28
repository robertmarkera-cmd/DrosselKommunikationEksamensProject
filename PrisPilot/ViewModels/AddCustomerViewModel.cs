using PrisPilot.Commands;
using PrisPilot.Models;
using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
using PrisPilot.Services.Peristence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PrisPilot.ViewModels
{
    public class AddCustomerViewModel : BaseViewModel<Customer>
    {

        private CustomerRepository _customerRepo = new();

        private string _selectedFilePath = string.Empty;
        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                if (_selectedFilePath == value) return;
                _selectedFilePath = value;
                OnPropertyChanged();
            }
        }

        private CustomerViewModel _currentCustomer;
        public CustomerViewModel CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                if (_currentCustomer == value) return;
                _currentCustomer = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToHomeViewCommand { get; }
        public ICommand OpenFileForAddCustomerCommand { get; }
        public ICommand AddCustomerCommand { get; }

        public AddCustomerViewModel(IFileDialogService fileDialogService) : base(new Customer())
        {
            OpenFileForAddCustomerCommand = new OpenFileForAddCustomerCommand(fileDialogService);
            AddCustomerCommand = new AddCustomerCommand();
            CurrentCustomer = new CustomerViewModel(Entity);
        }

        public void SetCurrentCustomerLogo()
        {
            if (string.IsNullOrWhiteSpace(SelectedFilePath)) return;

            ImageService imageService = new ImageService();
            byte[] bytes = imageService.ReadFileBytes(SelectedFilePath);
            CurrentCustomer.Logo = bytes;
        }

        public void AddToRepo()
        {
            _customerRepo.Add(
                 CurrentCustomer.Cvr,
                 CurrentCustomer.CompanyName,
                 CurrentCustomer.Email,
                 CurrentCustomer.TelephoneNumber,
                 CurrentCustomer.Address,
                 CurrentCustomer.Logo,
                 CurrentCustomer.ContactPerson,
                 CurrentCustomer.HourlyCost
             );
        }

    }
}
