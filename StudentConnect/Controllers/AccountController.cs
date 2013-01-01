using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StudentConnect.Utils;
using StudentConnect.Azure;

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

            var helper = ServiceProvider.Resolve<StorageHelper>();

            string username = helper.StandardUsername;

            if (helper.AdminPassword.Equals(passcode)) username = helper.AdminUsername;

            var validated = Membership.ValidateUser(username, passcode);
            if (validated)
            {

                // implement 'Remember Me' feature
                var remember = false;
                if (collection["rememberme"] != null) remember = true;
                FormsAuthentication.SetAuthCookie(username, remember);

                var schooldata = helper.Schools.FirstOrDefault(q => q.Passcode == passcode);
                if (schooldata != null) Session["_ActiveSchool"] = schooldata;

                //TODO: Reimplement this!
                // redirect to the returnUrl if it exists
                //if (!string.IsNullOrWhiteSpace(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                // otherwise go to Home.
                return RedirectToAction("Index", "Home");                
            }
            ViewBag.AuthError = "Invalid Passcode";
            return View();
            
        }
    }
}
