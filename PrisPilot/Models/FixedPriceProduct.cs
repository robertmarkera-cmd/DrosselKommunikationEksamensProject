using System;
using System.Collections.Generic;
using System.Text;
using PrisPilot.Services.Interfaces;

namespace PrisPilot.Models
{
    public class FixedPriceProduct : IProduct
    {
        public int FixedPriceProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Frequency { get; set; }

        // Stuff for interface
        public int Id { get {return FixedPriceProductID;}}
        ProductKind IProduct.Kind { get { return ProductKind.FixedPrice; } }
        public double ProductPrice { get { return Price; } }

    }
}
