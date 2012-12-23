using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace StudentConnect.Auth
{
    public class PortalRoleProvider : RoleProvider
    {
        private readonly Dictionary<string, List<String>> roleTable = new Dictionary<string, List<string>>();

        public PortalRoleProvider()
        {
            this.ApplicationName = "PortalRoleProvider";
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get; set;
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
            return this.roleTable.Keys.ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            return this.roleTable.Where(q => q.Value.Contains(username)).Select(q => q.Key).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            if (!this.RoleExists(roleName)) return new string[0];
            return this.roleTable.First(q => q.Key == roleName).Value.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (!this.RoleExists(roleName)) return false;
            return this.roleTable.First(q => q.Key == roleName).Value.Any(q => q == username);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return this.GetAllRoles().Contains(roleName);
        }
    }
}