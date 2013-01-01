using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Data;
using StudentConnect.Utils;

namespace StudentConnect.Controllers
{
    public class AboutController : Controller
    {
        private IContentRepository repo;

        public AboutController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
        }
        //
        // GET: /About/
        [Authorize(Roles = "Student, Admin")]
        public ActionResult Index()
        {

            return View(repo.GetAbout());
        }

    }
}
