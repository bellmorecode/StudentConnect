using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentConnect.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var passcode = collection["passcode"];
            bool authsuccess = false;
            bool attemptLogin = false;

            // TODO: Work in Progress
            switch(passcode)
            {
                case "RPI":
                    attemptLogin = true;
                    break;
                default:
                    break;
            }

            var result = Membership.ValidateUser("GlennStudentConnect", "M@st3rK3y");
            if (authsuccess && attemptLogin)
            {
                var passbackUrl = Request.QueryString["ddd"];
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to authentication. Unknown passcode.";
                return View();

            }
            
        }
    }
}
