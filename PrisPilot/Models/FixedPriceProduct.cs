using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Models
{
    public class FixedPriceProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Frequency { get; set; }

    }
}
