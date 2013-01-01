using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace StudentConnect.Data
{
    public class ContentRepository : IContentRepository
    {
        // for now. 
        IContentRepository mock = new MockContentRepository();

        public AboutContent GetAbout()
        {
            var school = (SchoolData)HttpContext.Current.Session["_ActiveSchool"];
            return mock.GetAbout();
        }

        public IEnumerable<Person> GetPeople()
        {
            return mock.GetPeople();
        }

        public IEnumerable<Position> GetPositions()
        {
            return mock.GetPositions();
        }

        public void SaveContact(ContactInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
