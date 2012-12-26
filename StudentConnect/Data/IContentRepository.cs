
namespace StudentConnect.Data
{
    using System.Collections.Generic;

    public interface IContentRepository
    {
        AboutContent GetAbout();
        IEnumerable<Person> GetPeople();
        IEnumerable<Position> GetPositions();
    }
}
