using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentConnect.Data
{
    public sealed class ContactInfo
    {
        public string FullName { get; set; }
        public string Major { get; set; }
        public string School { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
