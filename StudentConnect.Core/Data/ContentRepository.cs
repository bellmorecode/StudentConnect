using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using StudentConnect.Azure;
using StudentConnect.Utils;
namespace StudentConnect.Data
{
    public class ContentRepository : IContentRepository
    {
        StorageHelper store = ServiceProvider.Resolve<StorageHelper>();
        public ContentRepository()
        {

        }

        public AboutContent GetAbout()
        {
            var school = (SchoolData)HttpContext.Current.Session["_ActiveSchool"];
            SchoolMetadata data;
            if (school == null)
            {
                data = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            }
            else
            {
                data = store.GetSchoolMetadata(school.Alias);
                if (data == null) data = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            }
            return data.About;
        }

        public IEnumerable<Person> GetPeople()
        {
            var school = (SchoolData)HttpContext.Current.Session["_ActiveSchool"];
            SchoolMetadata data;
            if (school == null)
            {
                data = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            }
            else
            {
                data = store.GetSchoolMetadata(school.Alias);
                if (data == null) data = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            }
            return data.People;
        }

        public IEnumerable<Position> GetPositions()
        {
            var school = (SchoolData)HttpContext.Current.Session["_ActiveSchool"];
            SchoolMetadata data;
            if (school == null)
            {
                data = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            }
            else
            {
                data = store.GetSchoolMetadata(school.Alias);
                if (data == null) data = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            } 
            return data.Positions;
        }

        public void SaveContact(ContactInfo info)
        {
            store.AddRequesterSubmission(info.RequesterID, info);       
        }
    }
}
