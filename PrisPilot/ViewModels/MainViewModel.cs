using PrisPilot.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.ViewModels
{
    public class MainViewModel : SuperClassViewModel
    {
        private readonly NavigationStore _navigationStore;

        public SuperClassViewModel CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
        }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
