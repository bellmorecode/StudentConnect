using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace StudentConnect.Data
{
    [Serializable]
    public sealed class ContactInfo
    {
        public string FullName { get; set; }
        public string Major { get; set; }
        public string School { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public string Interests { get; set; }
        public string PreferredContactMethod { get; set; }
        public string GradYear { get; set; }
        public string JobType { get; set; }

        public string UploadKey { get; set; }

        public byte[] Attachment { get; set; }

        public string RequesterID { get; set; }

        public DateTime? LastUpdated { get; set; }

        public override string ToString()
        {
            try
            {
                var ser = new XmlSerializer(typeof(ContactInfo));
                using (var ms = new MemoryStream())
                {
                    ser.Serialize(ms, this);
                    return new String(Encoding.UTF8.GetChars(ms.GetBuffer()));
                }
            }
            catch (Exception )
            {
                return base.ToString();
            }
        }
    }
}
