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
    public class HomeController : Controller
    {
        IContentRepository repo;

        public HomeController()
        {
            // TODO: Ioc should be here.
            repo = new MockContentRepository();
        }
        
        public ActionResult Index()
        {
            int index = 1;
            var model = (from x in repo.GetPositions() select new PositionItem { Index = index++, Value = x.Title }).ToArray();
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveContactData(FormCollection collection)
        {

            return RedirectToAction("Index");
        }
    }
}
