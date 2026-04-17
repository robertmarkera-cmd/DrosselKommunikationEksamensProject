using PrisPilot.Commands;
using PrisPilot.Services;
using PrisPilot.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrisPilot.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigateToFirstViewCommand { get; }
        public ICommand NavigateToSecondViewCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigationService navigationServiceFirstView = new NavigationService(navigationStore, () => new FirstViewModel(navigationStore));
            NavigationService navigationServiceSecondView = new NavigationService(navigationStore, () => new SecondViewModel(navigationStore));

            NavigateToFirstViewCommand = new NavigateCommand(navigationServiceFirstView);
            NavigateToSecondViewCommand = new NavigateCommand(navigationServiceSecondView);
        }
    }
}
