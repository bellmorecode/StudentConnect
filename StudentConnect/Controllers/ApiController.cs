using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentConnect.Data;

namespace StudentConnect.Controllers
{
    public class ApiController : Controller
    {
        IContentRepository repo;

        public ApiController()
        {
            // TODO: Replace with IOC, once we are online!!
            repo = new StudentConnect.Data.MockContentRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PositionList()
        {
            return Json(repo.GetPositions().Select(q => q.Title).ToArray());
        }

        [HttpPost]
        public JsonResult PositionDetails()
        {
            return Json(repo.GetPositions());
        }
        
        [HttpPost]
        public JsonResult People()
        {
            return Json(repo.GetPeople());
        }

        [HttpPost]
        public JsonResult AboutContent()
        {
            return Json(repo.GetAbout());
        }
    }
}
