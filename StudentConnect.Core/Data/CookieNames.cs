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


        public static void SetResponseLifetime(HttpResponseBase resp, int lengthInDays)
        {
            resp.Cookies[LastUpdated].Expires = DateTime.Now.AddDays(lengthInDays);
            resp.Cookies[FullName].Expires = DateTime.Now.AddDays(lengthInDays);
        }
    }
}
