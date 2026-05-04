using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrisPilot.Models
{
    public class Customer
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Cvr { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        // We're storing the logo as a byte array, since storing it as an image file was a PITA.
        // We're also going to add it to our database as a bytearray anyways
        public byte[]? Logo { get; set; } = null;
        public string ContactPerson { get; set; } = string.Empty;
    }
    
}
