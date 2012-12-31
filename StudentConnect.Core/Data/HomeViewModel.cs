using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentConnect.Data
{
    public sealed class HomeViewModel
    {
        public HomeViewModel()
        {
            Metadata = new HomeMetadataViewModel();
            Info = new ContactInfo();
        }

        public ContactInfo Info { get; set; }

        public HomeMetadataViewModel Metadata { get; set; }
    }

    public sealed class HomeMetadataViewModel
    {
        public HomeMetadataViewModel()
        {
            Positions = new PositionItem[0];
        }
        public PositionItem[] Positions { get; set; }
    }
}
