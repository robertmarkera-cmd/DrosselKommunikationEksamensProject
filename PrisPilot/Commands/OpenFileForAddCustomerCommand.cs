using PrisPilot.Services;
using PrisPilot.Services.Interfaces;
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
            if (parameter is AddCustomerViewModel ACM)
            {
                string file = _fileDialogService.OpenFileDialog();
                if (!string.IsNullOrEmpty(file))
                {
                    try
                    {
                        ACM.SelectedFilePath = file;
                        ACM.SetCurrentCustomerLogo();

                    }
                    catch
                    {
                        throw new Exception("Failed to update CurrentCustomer.Logo to selected image");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Command expects an AddCostumerViewModel as parameter.");
            }
        }
    }
}
