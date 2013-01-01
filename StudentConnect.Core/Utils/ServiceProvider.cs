using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace StudentConnect.Utils
{
    public static class ServiceProvider
    {
        public static TService Resolve<TService>()
        {
            return new UnityContainer().LoadConfiguration().Resolve<TService>();
        }

        public static TService Resolve<TService>(string name)
        {
            return new UnityContainer().LoadConfiguration().Resolve<TService>(name);
        }
    }
}