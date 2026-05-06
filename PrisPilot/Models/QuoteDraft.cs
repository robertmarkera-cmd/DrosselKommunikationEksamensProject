using PrisPilot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Models
{
    public class QuoteDraft
    {
        public Customer? Customer { get; set; }

        public List<IProduct> Products { get; } = [];

        public double Subtotal { get; set; }
        public double Discount { get; set; }
        public double Total => Subtotal - Discount;
    }
}
