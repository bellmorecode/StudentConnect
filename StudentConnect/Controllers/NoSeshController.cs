using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentConnect.Controllers
{
    public class NoSeshController : Controller
    {
        //
        // GET: /NoSesh/

        public ActionResult Index()
        {
            Session["DO_NOT_SAVE"] = "TRUE";
            return RedirectToAction("Index", "Home");
        }

    }
}
