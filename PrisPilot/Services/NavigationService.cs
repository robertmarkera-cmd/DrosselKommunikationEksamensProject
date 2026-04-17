using PrisPilot.Stores;
using PrisPilot.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<SuperClassViewModel> _viewModelFactory;

        public NavigationService(NavigationStore navigationStore, Func<SuperClassViewModel> viewModelFactory)
        {
            _navigationStore = navigationStore;
            _viewModelFactory = viewModelFactory;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _viewModelFactory();
        }
    }
}
