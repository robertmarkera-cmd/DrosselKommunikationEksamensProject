using Microsoft.Win32;
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
    public class FirstViewModel : SuperClassViewModel
    {
        private readonly IFileDialogService _fileDialogService;

        public ICommand NavigateToHomeViewCommand { get; }

        public FirstViewModel(NavigationStore navigationStore)
        {
            //NavigationService navigationServiceHomeView = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));
            //NavigateToHomeViewCommand = new NavigateCommand(navigationServiceHomeView);
        }
    }
}
