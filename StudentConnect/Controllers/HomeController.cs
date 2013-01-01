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

            FillContactInfoFromCookies(model.Info);
            
            model.Metadata.Positions = (from x in repo.GetPositions() 
                                        select new PositionItem { Index = index++, Value = x.Title })
                                        .ToArray();

            return View(model);
        }

        private void FillContactInfoFromCookies(ContactInfo info)
        {
            if (Request.Cookies[CookieNames.LastUpdated] != null)
            {
                var nameCookie = Response.Cookies[CookieNames.FullName];
                var lastUpdatedCookie = Request.Cookies[CookieNames.LastUpdated];

                info.FullName = nameCookie.Value;
                info.LastUpdated = DateTime.Parse(lastUpdatedCookie.Value);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Student, Admin")]
        public ActionResult SaveContactData(FormCollection collection)
        {
            // add cookie
            Response.Cookies[CookieNames.LastUpdated].Value = DateTime.Now.ToString();
            Response.Cookies[CookieNames.FullName].Value = collection["name"];


            CookieNames.SetResponseLifetime(Response, 365);
            // save contact info

            // notify user

            return RedirectToAction("Index");
        }

        public HttpCookie lastUpdatedCookie { get; set; }
    }
}
