using PrisPilot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Models
{
    public class VariablePriceProduct : IProduct
    {
        public int VariablePriceProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Stuff for interface
        public int ProductID { get { return VariablePriceProductID; } }
        public double ProductPrice { get; set; }
    }
}
