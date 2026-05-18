using PrisPilot.Models;
using System;

namespace PrisPilot.ViewModels
{
    public class QuoteViewModel : BaseViewModel<Quote>
    {
        private Quote _quote;

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged();
            }
        }

        private int _hourlyCost;
        public int HourlyCost
        {
            get => _hourlyCost;
            set
            {
                if (_hourlyCost == value) return;
                _hourlyCost = value;
                OnPropertyChanged();
            }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (_totalPrice == value) return;
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        private int _quoteID;
        public int QuoteID
        {
            get => _quoteID;
            set
            {
                if (_quoteID == value) return;
                _quoteID = value;
                OnPropertyChanged();
            }
        }

        private string _cvr = string.Empty;
        public string Cvr
        {
            get => _cvr;
            set
            {
                if (_cvr == value) return;
                _cvr = value;
                OnPropertyChanged();
            }
        }

        private int _templateID;
        public int TemplateID
        {
            get => _templateID;
            set
            {
                if (_templateID == value) return;
                _templateID = value;
                OnPropertyChanged();
            }
        }

        private string _recentCost;
        public string RecentCost
        {
            get => _recentCost;
            set
            {
                if (_recentCost == value) return;
                _recentCost = value;
                OnPropertyChanged();
            }
        }

        public QuoteViewModel(Quote quote) : base(quote)
        {
            _quote = quote;

            Date = quote.Date;
            HourlyCost = quote.HourlyCost;
            TotalPrice = quote.TotalPrice;
            QuoteID = quote.QuoteID;
            Cvr = quote.Cvr ?? string.Empty;
            TemplateID = quote.TemplateID;
        }

        public bool IsQuoteValid()
        {
            bool result = true;

            // CVR: must be 8 digits
            if (string.IsNullOrWhiteSpace(Cvr) || Cvr.Length != 8 || !int.TryParse(Cvr, out _))
            {
                result = false;
            }

            // Hourly cost should be positive
            if (HourlyCost <= 0) result = false;

            // Total price should be non-negative
            if (TotalPrice < 0) result = false;

            // TemplateID should be set
            if (TemplateID < 0) result = false;

            return result;
        }

        public Quote ToModel()
        {
            _quote.Date = Date;
            _quote.HourlyCost = HourlyCost;
            _quote.TotalPrice = TotalPrice;
            _quote.QuoteID = QuoteID;
            _quote.Cvr = Cvr;
            _quote.TemplateID = TemplateID;

            return _quote;
        }
    }
}
