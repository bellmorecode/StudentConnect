using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Utils;
using StudentConnect.Azure;

namespace StudentConnect.Controllers
{
    public class ManageController : Controller
    {
        // GET: /Manage/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            var helper = ServiceProvider.Resolve<StorageHelper>();
            return View(helper.Schools);
        }

        public ActionResult School(string id)
        {
            return View();
        }
    }
}
