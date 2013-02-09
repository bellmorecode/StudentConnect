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

            data.Header.Passcode = "_" + SchoolMetadata.DefaultAlias;
            data.Header.Alias = SchoolMetadata.DefaultAlias;
            
            data.About.AboutUsHtml = "Something Digital (SD) is a dynamic, New York City-based Technology Services boutique offering three distinct practice groups—Interactive Design, Software, and IT Services—to meet diverse technology needs.";

            data.Positions.Add(new Position { Title = "Interactive Developer", Description = "A software developer works primarily with applications on the LAMP Stack including Wordpress, Drupal, and Magento. Interactive developers primarily work on corporate websites or e-Commerce sites." });
            data.Positions.Add(new Position { Title = "Software Developer", Description = "A software developer works primarily with applications on the .NET Stack including ASP.NET, SharePoint, SiteCore, WPF, Silverlight and Dynamics CRM. Software developers primarily create corporate line-of-business or collaborative web applications" });
            data.Positions.Add(new Position { Title = "Project Manager", Description = "A project manager works with SD's client and with all departments to drive projects to completeing while effective tracking and communicating project progress." });
            data.Positions.Add(new Position { Title = "Business Analyst", Description = "An analyst would work with the PM team and the project leads to determine the clients business and technical requirements for a project as well as assist with the quality assurance process." });

            data.People.Add(new Person { DisplayOrder = 1, Name = "Betsy Garcia", MoreInfo = "Betsy coordinates all activity or SD's Human Resources department.  Betsy will be your key contact if you plan to pursue a career with SD.", Title = "HR Facilitator" });
            data.People.Add(new Person { DisplayOrder = 2, Name = "James Idoni", MoreInfo = "James is the director of our Project Management office (PMO) and leads a group of PM's focused on delivery high-quality results to our clients on-time and on budget. James works with our Software, Interactive and IT teams.", Title = "Manager, PMO" });
            data.People.Add(new Person { DisplayOrder = 3, Name = "Glenn Ferrie", MoreInfo = "Glenn leads our Microsoft Business Productivity practice and is involved in both the business development and delivery aspects of the business. &lt;br/gt; LinkedIn: http://www.linkedin.com/in/glennferrie/", Title = "Practice Manager" });

            helper.UpdateSchoolMetadata(SchoolMetadata.DefaultAlias, data);
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
