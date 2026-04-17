using PrisPilot.Services;
using PrisPilot.Stores;
using PrisPilot.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrisPilot.Commands
{
    public class NavigateCommand : ICommand
    {
        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
