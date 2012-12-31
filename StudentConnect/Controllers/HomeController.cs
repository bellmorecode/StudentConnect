using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using StudentConnect.Data;

namespace StudentConnect.Controllers
{
    using StudentConnect.Utils;
    public class HomeController : Controller
    {
        IContentRepository repo;

        public HomeController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
        }

        [Authorize(Roles="Student, Admin")]
        public ActionResult Index()
        {
            int index = 1;
            
            var model = new HomeViewModel();
            
            // TODO: Replace Mock with either BLANK or FROM COOKIES
            // MOCK
            ////model.Info.FullName = "Glenn Ferrie";
            ////model.Info.Interests = "Software Developer,Project Manager";

            model.Metadata.Positions = (from x in repo.GetPositions() 
                                        select new PositionItem { Index = index++, Value = x.Title })
                                        .ToArray();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Student, Admin")]
        public ActionResult SaveContactData(FormCollection collection)
        {
            // TODO: Implement 'Save'
            return RedirectToAction("Index");
        }
    }
}
