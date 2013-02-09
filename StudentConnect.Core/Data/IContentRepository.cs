
namespace StudentConnect.Data
{
    using System.Collections.Generic;
    using StudentConnect.Data;
using System.IO;

    public interface IContentRepository
    {
        SchoolData Current { get; set; }
        AboutContent GetAbout();
        IEnumerable<Person> GetPeople();
        IEnumerable<Position> GetPositions();
        void SaveContact(ContactInfo info);
        void SaveAttachment(string path, Stream stream);
    }
}
