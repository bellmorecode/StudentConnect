using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Data;
using StudentConnect.Utils;

namespace StudentConnect.Controllers
{
    public class PositionsController : Controller
    {
        IContentRepository repo;

        public PositionsController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
        }

        //
        // GET: /Positions/
        [Authorize(Roles = "Student, Admin")]
        public ActionResult Index()
        {
            var model = repo.GetPositions();
            return View(model);
        }

    }
}
