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
        public ActionResult OwnAlbum()
        {
            return View("~/Views/Partial/Albums/CreateNew.cshtml");
        }

        [HttpPost]
        public ActionResult ExpandFoto(Guid fotoID)
        {
            var handler = new AlbumsHandler(); 
            FotoModel model = handler.GetFotoByGUID(GetCookiesValue(Request), fotoID);
            if (model != null)
            {
                var cultureInfo = System.Globalization.CultureInfo.CurrentCulture;

                ViewBag.fotoId = fotoID;
                ViewBag.fullFoto = model.FullFotoUri;
                if (!string.IsNullOrWhiteSpace(model.Title) && model.Title != "null")
                {
                    ViewBag.titl = model.Title;
                }
                else
                {
                    string value = Properties.Resources.ResourceManager.GetString("Text_Add_Title", cultureInfo);
                    var text = MvcHtmlString.Create(value);

                    ViewBag.titl = text;
                }
                if (!string.IsNullOrWhiteSpace(model.AuthorDescription) && model.AuthorDescription != "null")
                {
                    ViewBag.comm = model.AuthorDescription;
                }
                else
                {
                    string value = Properties.Resources.ResourceManager.GetString("Text_Add_Description", cultureInfo);
                    var text = MvcHtmlString.Create(value);

                    ViewBag.comm = text;
                }
                return View("~/Views/Partial/Albums/Expand.cshtml");
            }
            return RedirectToAction("notfound", "error");
        }

        [HttpPost]
        public ActionResult UploadFotoForm(Guid albumID)
        {
            ViewBag.album = albumID;
            return View("~/Views/Partial/Albums/UploadFotoForm.cshtml");
        }

        [HttpPost]
        public ActionResult AlbumReview(Guid album)
        {
            var handler = new AlbumsHandler();
            AlbumModel model = handler.GetAlbumByGUID(GetCookiesValue(Request), album);
            if (model != null)
            {
                return View("~/Views/Partial/Albums/AlbumReview.cshtml", model);
            }
            return RedirectToAction("notfound", "error");
        }

        [HttpPost]
        public ActionResult CommentEntry(List<FotoCommentModel> comments)
        {
            foreach (var item in  comments)
            {
                item.DateFormat = new DateTime(item.PostDate, DateTimeKind.Utc).ToLongDateString();
            }
            return View("~/Views/Partial/Albums/CommentEntry.cshtml", comments);
        }

        [HttpPost]
        public ActionResult EditInfo(Guid fotoID, int type)
        {
            ViewBag.fotoID = fotoID;
            ViewBag.type = type;
            if (type == 0)
            {
                ViewBag.idtype = "newt_";
                ViewBag.inptype = "edit_tit_";
            }
            else
            {
                ViewBag.idtype = "newd_";
                ViewBag.inptype = "edit_desc_";
            }
            return View("~/Views/Partial/Albums/EditFotoInfo.cshtml");
        }

        [HttpPost]
        public ActionResult FriendExpand(Guid fotoID)
        {
            var handler = new AlbumsHandler();
            FotoModel model = handler.GetFotoByGUID(GetCookiesValue(Request), fotoID);

            ViewBag.fotoId = model.ID;
            ViewBag.fullFoto = model.FullFotoUri;
            if (!string.IsNullOrWhiteSpace(model.Title) && model.Title != "null")
            {
                ViewBag.titl = model.Title;
            }
            else
            {
                ViewBag.titl = "";
            }
            if (!string.IsNullOrWhiteSpace(model.AuthorDescription) && model.AuthorDescription != "null")
            {
                ViewBag.comm = model.AuthorDescription;
            }
            else
            {
                ViewBag.comm = "";
            }
            return View("~/Views/Partial/Albums/FriendExpand.cshtml");
        }

        [HttpPost]
        public ActionResult Add_Education_Form()
        {
            return View("~/Views/Partial/OwnPage/AddInfo/education_form.cshtml");
        }

        [HttpPost]
        public ActionResult Hight_Education_Level_Form()
        {
            return View("~/Views/Partial/OwnPage/AddInfo/high_level_form.cshtml");
        }

        [HttpPost]
        public ActionResult Mem_Events_Form()
        {
            return View("~/Views/Partial/OwnPage/AddInfo/mem_events_form.cshtml");
        }

        [HttpPost]
        public ActionResult Work_Form()
        {
            return View("~/Views/Partial/OwnPage/AddInfo/work_form.cshtml");
        }

        [HttpPost]
        public ActionResult Places_Form()
        {
            return View("~/Views/Partial/OwnPage/AddInfo/places_form.cshtml");
        }

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
        public ActionResult Change_Parameter(UserParams param)
        {
            ViewBag.Num = (int)param;
            return View("~/Views/Partial/OwnPage/change_private_info.cshtml");
        }

        [HttpPost]
        public ActionResult Internal_Error()
        {
            return View("~/Views/Error/internal_part_error.cshtml");
        }

        [HttpPost]
        public ActionResult Await_Key_Generation()
        {
            string session = GetCookiesValue(this.Request);
            var handler = new UsersConnectionHandler();
            var cultureCode = handler.GetConnectionBySession(session).CultureCode[0];
            CultureInfo cul = CultureInfo.CreateSpecificCulture(cultureCode);
            string mess = Properties.Resources.ResourceManager.GetString("Text_Key_Generation", cul);
            ViewBag.BePatient = mess;
            return View("~/Views/Partial/CryptoDialogs/await_key_generation.cshtml");
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
    }
}