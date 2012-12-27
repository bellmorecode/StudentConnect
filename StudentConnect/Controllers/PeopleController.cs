using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Data;
using StudentConnect.Utils;

namespace StudentConnect.Controllers
{
    public class PeopleController : Controller
    {
        private IContentRepository repo;

        public PeopleController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
        }
        //
        // GET: /People/

        public ActionResult Index()
        {
            return View();
        }

    }
}
