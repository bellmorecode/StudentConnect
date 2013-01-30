using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Data;
using StudentConnect.Utils;
using StudentConnect.Azure;
using System.Web.Security;

namespace StudentConnect.Controllers
{
    public sealed class AuthRequest
    {
        public string Passcode { get; set; }
    }

    public sealed class SchoolDataRequest
    {
        public string SchoolAlias { get; set; }
    }

    public class ApiController : Controller
    {
        IContentRepository repo;

        public ApiController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
        }


        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Authenticate(AuthRequest req)
        {
            if (req == null) req = new AuthRequest();
            var helper = ServiceProvider.Resolve<StorageHelper>();
            SchoolData schooldata = new SchoolData();

            string username = helper.StandardUsername;

            if (helper.AdminPassword.Equals(req.Passcode)) username = helper.AdminUsername;

            var validated = Membership.ValidateUser(username, req.Passcode);
            if (validated)
            {
                schooldata = helper.Schools.FirstOrDefault(q => q.Passcode == req.Passcode);
                
            }
            
            return Json(new { IsValidated = validated, SchoolData = schooldata });
        }

        [HttpPost]
        public JsonResult PositionList(SchoolDataRequest req)
        {
            if (req == null) req = new SchoolDataRequest();
            var helper = ServiceProvider.Resolve<StorageHelper>();
            var current = helper.Schools.FirstOrDefault(q => q.Alias == req.SchoolAlias);
            if (current != null) repo.Current = current;
            return Json(repo.GetPositions().Select(q => q.Title).ToArray());
        }

        [HttpPost]
        public JsonResult PositionDetails(SchoolDataRequest req)
        {
            if (req == null) req = new SchoolDataRequest();
            var helper = ServiceProvider.Resolve<StorageHelper>();
            var current = helper.Schools.FirstOrDefault(q => q.Alias == req.SchoolAlias);
            if (current != null) repo.Current = current;
            return Json(repo.GetPositions());
        }
        
        [HttpPost]
        public JsonResult People(SchoolDataRequest req)
        {
            if (req == null) req = new SchoolDataRequest();
            var helper = ServiceProvider.Resolve<StorageHelper>();
            var current = helper.Schools.FirstOrDefault(q => q.Alias == req.SchoolAlias);
            if (current != null) repo.Current = current;
            return Json(repo.GetPeople());
        }

        [HttpPost]
        public JsonResult AboutContent(SchoolDataRequest req)
        {
            if (req == null) req = new SchoolDataRequest();
            var helper = ServiceProvider.Resolve<StorageHelper>();
            var current = helper.Schools.FirstOrDefault(q => q.Alias == req.SchoolAlias);
            if (current != null) repo.Current = current;
            return Json(repo.GetAbout());
        }

        public JsonResult SaveContact(ContactInfo info)
        {
            var helper = ServiceProvider.Resolve<StorageHelper>();
            helper.AddRequesterSubmission(info.RequesterID, info);
            return Json(new { result = "success" });
        }
    }
}
