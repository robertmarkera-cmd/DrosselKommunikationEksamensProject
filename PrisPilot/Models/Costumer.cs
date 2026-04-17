using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrisPilot.Models
{
    public class Costumer
    {
        public string CompanyName { get; set; }
        public int Cvr { get; set; }
        public string Email { get; set; }
        public int TelephoneNumber { get; set; }
        public string Address { get; set; }
        public Image Logo { get; set; }

        public Costumer()
        {

        }
    }
    
}
