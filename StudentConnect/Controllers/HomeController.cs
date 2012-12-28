using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using StudentConnect.Data;

namespace StudentConnect.Controllers
{
    using Model;
    using StudentConnect.Utils;
    public class HomeController : Controller
    {
        IContentRepository repo;

        public HomeController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
        }
        [Authorize]
        public ActionResult Index()
        {
            int index = 1;
            var model = (from x in repo.GetPositions() select new PositionItem { Index = index++, Value = x.Title }).ToArray();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveContactData(FormCollection collection)
        {
            // TODO: Implement 'Save'
            return RedirectToAction("Index");
        }
    }
}
