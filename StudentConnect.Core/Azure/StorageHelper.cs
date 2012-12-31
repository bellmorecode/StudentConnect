using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StudentConnect.Data;

namespace StudentConnect.Azure
{
    public sealed class StorageHelper
    {
        readonly string _adminpassword;
        readonly string _adminuser;
        readonly string _standarduser;

        private readonly SchoolData[] _schools;
        
        public StorageHelper()
        {
            _adminpassword = "pass@word1";
            _adminuser = "sd-administrator";
            _standarduser = "standard-student";

            try
            {
                var connstring = ConfigurationManager.AppSettings["AzureStorage"];
                var acct = CloudStorageAccount.Parse(connstring);
                var client = acct.CreateCloudBlobClient();
                var dir = client.GetContainerReference("studentconnect");
                var adminpwd = dir.GetBlobReferenceFromServer("_adminpassword");
                var adminpwdText = adminpwd.DownloadText();
                var schoolsRef = dir.GetBlobReferenceFromServer("_schools");
                var schoolsText = schoolsRef.DownloadText();

                _adminpassword = adminpwdText;
                _schools = this.ParseSchoolText(schoolsText);                    
            }
            finally
            {

            }
        }

        private SchoolData[] ParseSchoolText(string content)
        {
            return content.Split(new [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(q => q.Split(new [] { '|' }))
                        .Select(q => new SchoolData { Alias = q[0], Passcode = q[1] })
                        .ToArray();
        }

        public string AdminPassword { get { return this._adminpassword; } }
        public string AdminUsername { get { return this._adminuser; } }
        public string StandardUsername { get { return this._standarduser; } }

        public SchoolData[] Schools { get { return this._schools; } }
    }
}
