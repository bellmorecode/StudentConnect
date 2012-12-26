using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentConnect.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public JsonResult PositionList()
        {
            return Json(this.PositionData().Select(q => q.Title).ToArray());
        }

        public JsonResult PositionDetails()
        {
            return Json(this.PositionData());
        }

        private Position[] PositionData()
        {
            var pos1 = new Position { Title = "Software Developer", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            var pos2 = new Position { Title = "Interactive Developer", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            var pos3 = new Position { Title = "UX Designer", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            var pos4 = new Position { Title = "Project Manager", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vitae aliquet felis. Suspendisse potenti. Mauris bibendum felis non enim aliquet scelerisque. Nunc sit amet felis at risus ultrices vestibulum. Sed volutpat vehicula ligula, eu laoreet neque volutpat sit amet. Cras risus lacus, iaculis nec facilisis quis, auctor non dui. Vestibulum." };
            return new[] { pos1, pos2, pos3, pos4 };
        }

        public JsonResult People()
        {
            return Json(new { Test = "" });
        }

        public JsonResult AboutContent()
        {
            return Json(new { Test = "" });
        }
    }
}
