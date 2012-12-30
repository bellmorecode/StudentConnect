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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var passcode = collection["passcode"];
            var returnUrl = collection["returnUrl"];
            string username = "StudentConnect_AppHarbor";

            if (passcode == "!pass@word1~") username = "GlennAdmin";

            var validated = Membership.ValidateUser(username, passcode);
            if (validated)
            {
                var remember = false;
                if (collection["rememberme"] != null) remember = true;
                FormsAuthentication.SetAuthCookie(username, remember);

                // TODO: Let's check of there is another path to redirect to??
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");

                
            }

            // TODO: set failure message

            return View();
            
        }
    }
}
