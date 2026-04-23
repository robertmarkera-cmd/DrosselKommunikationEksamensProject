using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
using PrisPilot.Stores;
using PrisPilot.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrisPilot.Commands
{
    public class OpenFileForAddCustomerCommand : ICommand
    {
        private readonly IFileDialogService _fileDialogService;

        public OpenFileForAddCustomerCommand(IFileDialogService fileDialogService)
        {
            _fileDialogService = fileDialogService;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is AddCostumerViewModel ACM)
            {
                string file = _fileDialogService.OpenFileDialog();
                if (!string.IsNullOrEmpty(file))
                {
                    ACM.SelectedFilePath = file;
                }
            }
            else
            {
                throw new ArgumentException("Command expects an AddCostumerViewModel as parameter.");
            }
        }
    }
}
