using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentConnect.Data
{
    [Serializable]
    public sealed class SchoolMetadata
    {
        public const string DefaultAlias = "_Default";
        public static SchoolMetadata Empty
        {
            get
            {
                var e = new SchoolMetadata();
                e.Header.Passcode = "password";
                e.About.AboutUsHtml = "Something Digital (SD) is a dynamic, New York City-based Technology Services boutique offering three distinct practice groups—Interactive Design, Software, and IT Services—to meet diverse technology needs.";
                return e;
            }
        }
        public SchoolMetadata()
        {
            Header = new SchoolData();
            Positions = new List<Position>();
            People = new List<Person>();
            About = new AboutContent();
        }
        public SchoolData Header { get; set; }
        public List<Position> Positions { get; set; }
        public List<Person> People { get; set; }
        public AboutContent About { get; set; }
    }
}
