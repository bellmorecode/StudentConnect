using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StudentConnect.Data;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;

namespace StudentConnect.Azure
{
    public sealed class StorageHelper
    {
        readonly string _adminpassword;
        readonly string _adminuser;
        readonly string _standarduser;

        readonly CloudBlobClient client;

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
                client = acct.CreateCloudBlobClient();
                var dir = client.GetContainerReference("studentconnect");
                var adminpwd = dir.GetBlobReferenceFromServer("_adminpassword");
                // If you get a 403 - Forbidden warning here, its because you dont have access to SD's Azure Storage account.
                // Get your own FREE here.  http://www.windowsazure.com/en-us/pricing/free-trial/
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

        private SchoolMetadata[] GetAllMetadata()
        {
            var dir = client.GetContainerReference("studentconnect");
            var metadataContent = dir.GetBlobReferenceFromServer("_metadata");
            var xml = metadataContent.DownloadText();
            var ser = new XmlSerializer(typeof(SchoolMetadata[]));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml.ToCharArray())))
            {
                var arr = (SchoolMetadata[])ser.Deserialize(ms);
                return arr;
            }
        }

        private void UpdateAllMetadata(SchoolMetadata[] data)
        {
            var dir = client.GetContainerReference("studentconnect");
            var metadataContent = dir.GetBlobReferenceFromServer("_metadata");

            var ser = new XmlSerializer(typeof(SchoolMetadata[]));
            using (var ms = new MemoryStream())
            {
                ser.Serialize(ms, data);
                ms.Position = 0;
                metadataContent.UploadFromStream(ms);
            }
        }

        // get / put school metadata
        public SchoolMetadata GetSchoolMetadata(string alias)
        {
            return this.GetAllMetadata().FirstOrDefault(q => q.Header.Alias == alias);
        }

        public void UpdateSchoolMetadata(string alias, SchoolMetadata data)
        {
            var all = new List<SchoolMetadata>(this.GetAllMetadata());
            var match = all.FirstOrDefault(q => q.Header.Alias == alias);
            if (match != null)
            {
                all.Remove(match);
            }
            all.Add(data);
            this.UpdateAllMetadata(all.ToArray());
        }

        public void AddRequesterAttachment(string path, Stream stream)
        {
            Task.Factory.StartNew(() =>
            {
                var dir = client.GetContainerReference("studentconnect-submissions");
                var attachment = dir.GetBlockBlobReference(path);
                attachment.UploadFromStream(stream);

            }).Wait();
        }

        public void AddRequesterSubmission(string requesterId, ContactInfo submission)
        {
            var dir = client.GetContainerReference("studentconnect-submissions");
            var submissions = dir.GetBlockBlobReference(requesterId);
            
            if (submissions.Exists())
            {
                var xml = submissions.DownloadText();
                var ser = new XmlSerializer(typeof(RequesterSubmissions));
                RequesterSubmissions rs;
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml.ToCharArray())))
                {
                    rs = (RequesterSubmissions)ser.Deserialize(ms);
                    // submit
                    rs.Submissions.Add(submission);
                }

                using (var ms = new MemoryStream())
                {
                    ser.Serialize(ms, rs);
                    // submit
                    ms.Position = 0;
                    submissions.UploadFromStream(ms);
                }
            }
            else
            {
                // create empty element
                var rs = new RequesterSubmissions() { RequesterID = requesterId };
                // add data
                rs.Submissions.Add(submission);                
                // serialize
                var ser = new XmlSerializer(typeof(RequesterSubmissions));
                using (var ms = new MemoryStream())
                {
                    ser.Serialize(ms, rs);
                    // submit
                    ms.Position = 0;
                    submissions.UploadFromStream(ms);
                }
                
            }
        }
    }
}
