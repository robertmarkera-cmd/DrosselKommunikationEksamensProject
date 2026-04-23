using PrisPilot.Commands;
using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
using PrisPilot.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrisPilot.ViewModels
{
    public class AddCostumerViewModel : SuperClassViewModel
    {
        private readonly IFileDialogService _fileDialogService;

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

        public ICommand NavigateToHomeViewCommand { get; }
        public ICommand OpenFileForAddCustomerCommand { get; }

        public AddCostumerViewModel(IFileDialogService fileDialogService)
        {
            OpenFileForAddCustomerCommand = new OpenFileForAddCustomerCommand(fileDialogService);
        }

    }
}
