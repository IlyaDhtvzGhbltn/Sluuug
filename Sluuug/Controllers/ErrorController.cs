using Slug.Context.ServerError;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slug.Controllers
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

        [HttpGet]
        public ActionResult ooops()
        {
            return View();
        }

        [HttpGet]
        public JsonResult custom(int error)
        {
            Response.Headers.Add("Error", "1");
            return new JsonResult()
            {
                Data = ServerErrorCodesMessagesDictionary.Error[error],
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            }; 
        }

        [HttpGet]
        public ActionResult user_already_exist()
        {
            return View();
        }
    }
}