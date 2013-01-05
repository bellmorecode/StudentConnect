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

        public ActionResult School(string id)
        {
            var header = store.Schools.FirstOrDefault(q => q.Alias == id);
            if (header == null) return RedirectToAction("UnknownSchool", new { id = id });
            var metadata = store.GetSchoolMetadata(id);
            if (metadata != null) return View(metadata);

            metadata = store.GetSchoolMetadata(SchoolMetadata.DefaultAlias);
            // set header info for current school on default metadata.
            return View(metadata);
        }

        public ActionResult UnknownSchool(string id)
        {
            dynamic m = new ExpandoObject();
            m.BogusID = id;
            return View(m);
        }
    }
}
