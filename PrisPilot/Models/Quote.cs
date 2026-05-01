using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Models
{
    public class Quote
    {
        public DateTime Date { get; set; }

        public int HourlyCost { get; set; }

        public double TotalPrice { get; set; }
    }
}
