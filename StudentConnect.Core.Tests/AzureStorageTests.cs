using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StudentConnect.Data;
using System.Xml.Serialization;
using System.IO;
using StudentConnect.Azure;

namespace StudentConnect.Core.Tests
{
    [TestClass]
    public class AzureStorageTests
    {
        CloudBlobClient client;
        
        [TestInitialize]
        public void SpinUpCloudStorageClient()
        {
            var connstring = ConfigurationManager.AppSettings["AzureStorage"];
            var acct = CloudStorageAccount.Parse(connstring);
            client = acct.CreateCloudBlobClient();
        }

        [TestMethod]
        public void Create_Metadata_Baseline()
        {
            var dir = client.GetContainerReference("studentconnect");
            var metadataContent = dir.GetBlobReferenceFromServer("_metadata");
            var xml = metadataContent.DownloadText();
            if (string.IsNullOrWhiteSpace(xml))
            {
                var empty = new SchoolMetadata[0];
                var ser = new XmlSerializer(typeof(SchoolMetadata[]));
                using (var ms = new MemoryStream())
                {
                    ser.Serialize(ms, empty);
                    xml = new String(Encoding.UTF8.GetChars(ms.GetBuffer()));
                    Assert.AreNotEqual(string.Empty, xml);
                    ms.Position = 0;
                    metadataContent.UploadFromStream(ms);  
                }
                
            }
        }

        [TestMethod]
        public void Create_Default_SchoolMetadata()
        {
            var helper = new StorageHelper();
            var data = new SchoolMetadata();

            data.Header.Passcode = "_Default_";
            data.Header.Alias = "_Default";
            
            data.About.AboutUsHtml = "Something Digital (SD) is a dynamic, New York City-based Technology Services boutique offering three distinct practice groups—Interactive Design, Software, and IT Services—to meet diverse technology needs.";

            data.Positions.Add(new Position { Title = "Software Developer", Description = "A software developer works primarily with applications on the .NET Stack including ASP.NET, SharePoint, WPF, Silverlight and Dynamics CRM." });
            data.Positions.Add(new Position { Title = "Project Manager", Description = "A project manager works with SD's client and with all departments to drive projects to completeing while effective tracking and communicating project progress." });

            data.People.Add(new Person { DisplayOrder = 1, Name = "Betsy Garcia", MoreInfo = "Betsy has been with SD for 2 years and manages our Human Resources department.  Betsy will be your key contact if you plan to pursue a career with SD.", Title = "HR Manager", ImageUrl = "https://sdshare.blob.core.windows.net/res/logo.png" });
            data.People.Add(new Person { DisplayOrder = 2, Name = "Glenn Ferrie", MoreInfo = "Glenn runs our Microsoft Business Productivity practice and is involved in both the business development and deliery aspects of the business.", Title = "Practice Manager", ImageUrl = "https://sdshare.blob.core.windows.net/res/logo.png" });
            
            helper.UpdateSchoolMetadata("_Default", data);
        }

        [TestMethod]
        public void Create_Requester_Submission()
        {
            var info = new ContactInfo { 
                 About = "About me", RequesterID = "FAKE", Major = "Biology", School = "RPI", EmailAddress = "glenn@sample.com", FullName = "Glenn Ferrie",
                  Interests = "Software Developer", LastUpdated = DateTime.Now, PhoneNumber = "2125551212", PreferredContactMethod = "Email"
            };

            var helper = new StorageHelper();
            helper.AddRequesterSubmission(info.RequesterID, info);
        }
    }
}
