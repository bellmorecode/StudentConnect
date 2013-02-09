using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentConnect.Data
{
    // HACK: This is a Mock! repository
    public sealed class MockContentRepository : IContentRepository 
    {
        public IEnumerable<Position> GetPositions()
        {
            var pos1 = new Position { Title = "Software Developer", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            var pos2 = new Position { Title = "Interactive Developer", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            var pos3 = new Position { Title = "UX Designer", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            var pos4 = new Position { Title = "Project Manager", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            yield return pos1;
            yield return pos2;
            yield return pos3;
            yield return pos4;
        }

        public AboutContent GetAbout()
        {
            return new AboutContent { AboutUsHtml = "Something Digital (SD) is a dynamic, New York City-based Technology Services boutique offering three distinct practice groups—Interactive Design, Software, and IT Services—to meet diverse technology needs." };
        }

        public IEnumerable<Person> GetPeople()
        {
            yield return new Person { DisplayOrder = 1, Name = "Betsy Garcia", Title = "HR Director", MoreInfo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet.", BioLink = "", ImageUrl = "https://lh4.googleusercontent.com/-v-WVQRQf46M/AAAAAAAAAAI/AAAAAAAAAAA/ICaYpxmOnzw/s27-c/photo.jpg" };
            yield return new Person { DisplayOrder = 2, Name = "Glenn Ferrie", Title = "Practice Manager", MoreInfo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet.", BioLink = "", ImageUrl = "https://lh4.googleusercontent.com/-v-WVQRQf46M/AAAAAAAAAAI/AAAAAAAAAAA/ICaYpxmOnzw/s27-c/photo.jpg" };
            yield return new Person { DisplayOrder = 3, Name = "James Idoni", Title = "Project Manager", MoreInfo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet.", BioLink = "", ImageUrl = "https://lh4.googleusercontent.com/-v-WVQRQf46M/AAAAAAAAAAI/AAAAAAAAAAA/ICaYpxmOnzw/s27-c/photo.jpg" };
            yield return new Person { DisplayOrder = 3, Name = "Kishore Joshi", Title = "Software Dev", MoreInfo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet.", BioLink = "", ImageUrl = "https://lh4.googleusercontent.com/-v-WVQRQf46M/AAAAAAAAAAI/AAAAAAAAAAA/ICaYpxmOnzw/s27-c/photo.jpg" };
        }

        public void SaveContact(ContactInfo info)
        {
            // do nothing. saving. but not really.
        }



        public SchoolData Current
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }





        public void SaveAttachment(string path, System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}