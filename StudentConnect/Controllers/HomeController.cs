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
        PositionItem[] posList;
        int index = 1;

        public HomeController()
        {
            repo = ServiceProvider.Resolve<IContentRepository>();
            posList = (from x in repo.GetPositions()
                       select new PositionItem { Index = index++, Value = x.Title })
                                        .ToArray();
        }

        [Authorize(Roles="Student, Admin")]
        public ActionResult Index()
        {            
            var model = new HomeViewModel();
            FillContactInfoFromCookies(model.Info);
            model.Metadata.Positions = posList;
            return View(model);
        }

        // get saved data from cookies
        private void FillContactInfoFromCookies(ContactInfo info)
        {
            if (Request.Cookies[CookieNames.LastUpdated] != null)
            {
                var nameCookie = Request.Cookies[CookieNames.FullName];
                var emailCookie = Request.Cookies[CookieNames.EmailAddress];
                var phoneCookie = Request.Cookies[CookieNames.Phone];
                var majorCookie = Request.Cookies[CookieNames.Major];
                var aboutCookie = Request.Cookies[CookieNames.About];
                var intsCookie = Request.Cookies[CookieNames.Interests];
                var pcmCookie = Request.Cookies[CookieNames.PreferredContactMethod];
                var ridCookie = Request.Cookies[CookieNames.RequesterID];
                var lastUpdatedCookie = Request.Cookies[CookieNames.LastUpdated];
                var gradYearCookie = Request.Cookies[CookieNames.GradYear];
                var jobTypeCookie = Request.Cookies[CookieNames.JobType];

                info.FullName = nameCookie == null ? string.Empty : nameCookie.Value;
                info.EmailAddress = emailCookie  == null ? string.Empty : emailCookie.Value;
                info.PhoneNumber = phoneCookie == null ? string.Empty : phoneCookie.Value;
                info.Major = majorCookie == null ? string.Empty : majorCookie.Value;
                info.About = aboutCookie == null ? string.Empty : aboutCookie.Value;
                info.Interests = intsCookie == null ? string.Empty : intsCookie.Value;
                info.PreferredContactMethod = pcmCookie == null ? string.Empty : pcmCookie.Value;
                info.RequesterID = ridCookie == null ? string.Empty : ridCookie.Value;
                info.GradYear = gradYearCookie == null ? string.Empty : gradYearCookie.Value;
                info.JobType = jobTypeCookie == null ? string.Empty : jobTypeCookie.Value;
                info.LastUpdated = lastUpdatedCookie == null ? null : new DateTime?(DateTime.Parse(lastUpdatedCookie.Value));

                if (string.IsNullOrWhiteSpace(info.RequesterID))
                    info.RequesterID = Guid.NewGuid().ToString();
            }
            else
            {
                info.RequesterID = Guid.NewGuid().ToString();
            }
        }

        [Authorize(Roles = "Student, Admin")]
        public ActionResult Logout()
        {
            return RedirectToAction("Logout", "Account");
        }

        [HttpPost]
        [Authorize(Roles = "Student, Admin")]
        public ActionResult SaveContactData(FormCollection collection)
        {
            #region Util function
            // utility function
            Func<FormCollection, string> __getInterests = c =>
            {
                return string.Join(",", posList.Where(q => c[q.OptionName] != null).Select(a => a.Value).ToArray<object>());
                // NOTE: casted as object array to eliminate method abiguity
            };
            #endregion

            // scrape form data
            var info = new ContactInfo();
            info.FullName = collection["name"];
            info.EmailAddress = collection["email"];
            info.PhoneNumber = collection["phone"];
            info.Major = collection["major"];
            info.About = collection["about"];
            info.PreferredContactMethod = collection["contact-pref"];
            info.Interests = __getInterests(collection);
            info.LastUpdated = DateTime.Now;
            info.RequesterID = collection["requesterid"];
            if (Session["_ActiveSchool"] != null) {
                var data = (SchoolData)Session["_ActiveSchool"];
                info.School = data.Alias;
            } 

            // add cookies
            Response.Cookies[CookieNames.LastUpdated].Value = info.LastUpdated.ToString();
            Response.Cookies[CookieNames.FullName].Value = info.FullName;
            Response.Cookies[CookieNames.EmailAddress].Value = info.EmailAddress;
            Response.Cookies[CookieNames.Phone].Value = info.PhoneNumber;
            Response.Cookies[CookieNames.Major].Value = info.Major;
            Response.Cookies[CookieNames.About].Value = info.About;
            Response.Cookies[CookieNames.Interests].Value = info.Interests;
            Response.Cookies[CookieNames.PreferredContactMethod].Value = info.PreferredContactMethod;
            Response.Cookies[CookieNames.RequesterID].Value = info.RequesterID;

            CookieNames.SetResponseLifetime(Response, 365); // in days

            // save contact info
            repo.SaveContact(info);
            return RedirectToAction("Index");
        }
    }
}
