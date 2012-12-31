using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentConnect.Security
{
    public sealed class RoleProvider : System.Web.Security.RoleProvider
    {
        private Dictionary<string, List<string>> userroles = new Dictionary<string, List<string>>();
        public RoleProvider()
        {
            this.ApplicationName = "Default";
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var role in roleNames)
            {
                if (userroles.ContainsKey(role))
                {
                    var list = userroles[role];
                    foreach (var user in usernames)
                    {
                        if (list.Contains(user)) continue;
                        list.Add(user);
                    }
                    userroles[role] = list;
                }
                else
                {
                    userroles.Add(role, new List<string>(usernames));
                }
            }
        }
        public override string ApplicationName
        {
            get;
            set;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            return userroles.Keys.Where(key => userroles[key].Contains(username)).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}