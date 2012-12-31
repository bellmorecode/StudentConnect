using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace StudentConnect.Utils
{
    public static class DevTools
    {
        public static bool ShowLogoutLink()
        {
            try
            {
                var data = ConfigurationManager.AppSettings["StudentConnect.Utils.DevTools.ShowLogoutLink"];
                return bool.Parse(data);
            }
            catch (Exception)
            {
                // who cares
            }
            return false;
        }
    }
}