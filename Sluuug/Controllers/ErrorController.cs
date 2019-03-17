using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sluuug.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult notfound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult unauthorized()
        {
            return View();
        }
    }
}