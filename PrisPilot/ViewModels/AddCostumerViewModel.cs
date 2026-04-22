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

        public ICommand NavigateToHomeViewCommand { get; }
        public ICommand OpenFileForAddCustomerCommand { get; }

        public string SelectedFilePath { get; set; } = string.Empty;

        public AddCostumerViewModel(IFileDialogService fileDialogService)
        {
            OpenFileForAddCustomerCommand = new OpenFileForAddCustomerCommand(fileDialogService);
        }

    }
}
