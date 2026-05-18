using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Models
{
    public class TimeSpent
    {
        public int QuoteID { get; set; }
        public int VariablePriceProductID { get; set; }
        public int HoursUsed { get; set; }
    }
}
