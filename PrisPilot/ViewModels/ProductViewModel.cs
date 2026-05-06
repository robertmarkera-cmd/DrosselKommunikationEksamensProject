using System;
using PrisPilot.Models;
using PrisPilot.Services.Interfaces;

namespace PrisPilot.ViewModels
{
    public class ProductViewModel : SuperClassViewModel
    {
        private readonly IProduct _product;

        public IProduct Product => _product;
        public string Name => _product.Name;
        public ProductKind Kind => _product.Kind;

        //This is our TimeModel property. It gives us something to bind the UI to.
        public TimeSpent TimeSpentModel { get; private set; }

        public int HoursUsed
        {
            // returns HoursUsed if it's not null, else it returns 0
            get => TimeSpentModel?.HoursUsed ?? 0;
            set
            {
                // checks if timespentmodel exists and if the new value is the same as the old value
                if (TimeSpentModel != null && TimeSpentModel.HoursUsed != value)
                {
                    TimeSpentModel.HoursUsed = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    IsSelectedChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        public event EventHandler IsSelectedChanged;

        public ProductViewModel(IProduct product)
        {
            _product = product;

            // Automatically setup a TimeSpent model if applicable
            if (_product.Kind == ProductKind.VariablePrice)
            {
                TimeSpentModel = new TimeSpent 
                { 
                    VariablePriceProductID = _product.Id 
                };
            }
        }
    }
}