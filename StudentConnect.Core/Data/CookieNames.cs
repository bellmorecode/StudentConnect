using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace StudentConnect.Data
{
    public static class CookieNames
    {
        public const string LastUpdated = "StudentConnect_LastUpdated";
        public const string FullName = "StudentConnect_FullName";
        public const string EmailAddress = "StudentConnect_EmailAddress";
        public const string Phone = "StudentConnect_Phone";
        public const string Major = "StudentConnect_Major";
        public const string About = "StudentConnect_About";
        public const string Interests = "StudentConnect_Interests";
        public const string PreferredContactMethod = "StudentConnect_PreferredContactMethod";
        public const string RequesterID = "StudentConnect_RequesterID";
        public const string GradYear = "StudentConnect_GradYear";
        public const string JobType = "StudentConnect_JobType";



        public static void SetResponseLifetime(HttpResponseBase resp, int lengthInDays)
        {
            resp.Cookies[LastUpdated].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[FullName].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[EmailAddress].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[Phone].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[Major].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[About].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[Interests].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[PreferredContactMethod].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[RequesterID].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[GradYear].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[JobType].Expires = DateTime.Now.AddDays(lengthInDays);
        }

        
    }
}
