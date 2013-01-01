using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentConnect.Data
{
    [Serializable]
    public sealed class SchoolData
    {
        public string Alias { get; set; }
        public string Passcode { get; set; }
    }
}
