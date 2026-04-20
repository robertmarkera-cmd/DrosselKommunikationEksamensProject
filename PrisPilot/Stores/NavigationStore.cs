using PrisPilot.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Stores
{
    public class NavigationStore
    {
        private SuperClassViewModel _currentViewModel;
        public SuperClassViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
