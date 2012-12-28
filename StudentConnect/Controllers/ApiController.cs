using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Data;
using StudentConnect.Utils;

namespace StudentConnect.Controllers
{
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
        [Authorize]
        public JsonResult PositionList()
        {
            return Json(repo.GetPositions().Select(q => q.Title).ToArray());
        }

        [HttpPost]
        [Authorize]
        public JsonResult PositionDetails()
        {
            return Json(repo.GetPositions());
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult People()
        {
            return Json(repo.GetPeople());
        }

        [HttpPost]
        [Authorize]
        public JsonResult AboutContent()
        {
            return Json(repo.GetAbout());
        }
    }
}
