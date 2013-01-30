
namespace StudentConnect.Data
{
    using System.Collections.Generic;
    using StudentConnect.Data;

    public interface IContentRepository
    {
        SchoolData Current { get; set; }
        AboutContent GetAbout();
        IEnumerable<Person> GetPeople();
        IEnumerable<Position> GetPositions();
        void SaveContact(ContactInfo info);
    }
}
