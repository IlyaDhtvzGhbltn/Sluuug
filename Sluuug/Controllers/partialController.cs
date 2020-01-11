using Slug.Context.Dto.Albums;
using Slug.Context.Tables;
using Slug.Helpers;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Slug.Extensions;
using Slug.Context.Dto;
using Slug.Model;
using System.Globalization;

namespace Slug.Controllers
{
    public class partialController : SlugController
    {

        [HttpPost]
        public ActionResult Login_Already_Taken()
        {
            return View("~/Views/Partial/Register/login_already_taken.cshtml");
        }

        [HttpPost]
        public ActionResult Email_Already_Taken()
        {
            return View("~/Views/Partial/Register/email_already_taken.cshtml");
        }


        [HttpPost]
        public ActionResult Internal_Error()
        {
            return View("~/Views/Error/internal_part_error.cshtml");
        }

        [HttpPost]
        public ActionResult success_register()
        {
            return View("~/Views/Partial/Register/success_register.cshtml");
        }

        [HttpPost]
        public ActionResult fail_register()
        {
            return View("~/Views/Partial/Register/fail_register.cshtml");
        }

        [HttpPost]
        public ActionResult feedback()
        {
            return View("~/Views/Partial/feed_backView.cshtml");
        }

        [HttpPost]
        public ActionResult payment()
        {
            return View("~/Views/Partial/Payment/payment.cshtml");
        }

        [HttpGet]
        public ActionResult payments()
        {
            ViewBag.Referal = "55555";
            return View("~/Views/Partial/Payment/payment.cshtml");
        }
    }
}