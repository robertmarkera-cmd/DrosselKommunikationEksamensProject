using PrisPilot.Services.Interfaces;
using PrisPilot.ViewModels;
using System.Collections.Generic;

namespace PrisPilot.Models
{
    public class QuoteDraft
    {
        public Customer? Customer { get; set; }

        // This is an IEnumerable so we can just pass our observablecollection to it
        public IEnumerable<ProductViewModel> Products { get; set; } = [];

        public double Subtotal { get; set; }
        public double Discount { get; set; }
        public double Total => Subtotal - Discount;
        
        public int HourlyCost { get; set; }
    }
}
