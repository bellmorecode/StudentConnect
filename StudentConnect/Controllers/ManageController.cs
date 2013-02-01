using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Utils;
using StudentConnect.Azure;
using System.Dynamic;
using StudentConnect.Data;

namespace StudentConnect.Controllers
{
    public class ManageController : Controller
    {
        StorageHelper store;

        public ManageController()
        {
            store = ServiceProvider.Resolve<StorageHelper>();
        }

        // GET: /Manage/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View(store.Schools);
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult School(string id)
        {
            var header = store.Schools.FirstOrDefault(q => q.Alias == id);
            if (header == null && id != SchoolMetadata.DefaultAlias) return RedirectToAction("UnknownSchool", new { id = id });
            var metadata = store.GetSchoolMetadata(id);
            if (metadata != null) return View(metadata);

            metadata = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            metadata.Header.Passcode = header.Passcode;
            metadata.Header.Alias = header.Alias;
            store.UpdateSchoolMetadata(header.Alias, metadata);
            // set header info for current school on default metadata.
            return View(metadata);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult School(string id, FormCollection collection)
        {
            var passcode = collection["passcode"];
            var about = collection["about"];
            var logourl = collection["logourl"];


            var metadata = store.GetSchoolMetadata(id);
            metadata.Header.Passcode = passcode;
            metadata.About.AboutUsHtml = about;
            metadata.About.ImageUrl = logourl;
            


            store.UpdateSchoolMetadata(id, metadata);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UnknownSchool(string id)
        {
            dynamic m = new ExpandoObject();
            m.BogusID = id;
            return View(m);
        }
    }
}
