using PrisPilot.Commands;
using PrisPilot.Services;
using PrisPilot.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrisPilot.ViewModels
{
    public class HomeViewModel : SuperClassViewModel
    {
        public ICommand NavigateToFirstViewCommand { get; }
        public ICommand NavigateToSecondViewCommand { get; }

        public HomeViewModel(NavigationStore navigationStore, FileDialogService fileDialogService)
        {
            NavigationService navigationServiceFirstView = new NavigationService(navigationStore, () => new FirstViewModel(navigationStore));
            NavigationService navigationServiceSecondView = new NavigationService(navigationStore, () => new SecondViewModel(navigationStore));

            NavigationService navigationServiceAddCostumerView = new NavigationService(navigationStore, () => new AddCostumerViewModel(fileDialogService));


            // original navigate to first view
            //NavigateToFirstViewCommand = new NavigateCommand(navigationServiceFirstView);
            NavigateToFirstViewCommand = new NavigateCommand(navigationServiceAddCostumerView);
            NavigateToSecondViewCommand = new NavigateCommand(navigationServiceSecondView);
        }
    }
}
