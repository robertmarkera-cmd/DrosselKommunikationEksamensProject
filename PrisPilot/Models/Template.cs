using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Models
{
    public class Template
    {
        public int TemplateID { get; set; }
        public string Introduction { get; set; }
        public string TimeTable { get; set; }
        public bool AboutUs { get; set; }
        public string Title { get; set; }
        public string DrosselMail { get; set; }
        public string DrosselPhoneNumber { get; set; }
        public byte[] DrosselLogo { get; set; }


    }
}
