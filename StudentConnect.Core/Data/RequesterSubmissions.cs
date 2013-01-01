using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentConnect.Data
{
    [Serializable]
    public sealed class RequesterSubmissions
    {
        public RequesterSubmissions()
        {
            Submissions = new List<ContactInfo>();
        }
        public string RequesterID { get; set; }

        public List<ContactInfo> Submissions { get; set; }
    }
}
