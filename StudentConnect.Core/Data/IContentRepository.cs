
namespace StudentConnect.Data
{
    using System.Collections.Generic;
    using StudentConnect.Data;

    public interface IContentRepository
    {
        AboutContent GetAbout();
        IEnumerable<Person> GetPeople();
        IEnumerable<Position> GetPositions();
        void SaveContact(ContactInfo info);
    }
}
