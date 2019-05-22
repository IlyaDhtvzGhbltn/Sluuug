using Slug.Context.Dto.Albums;
using Slug.Context.Tables;
using Slug.Helpers;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                ViewBag.fotoId = fotoID;
                ViewBag.fullFoto = model.FullFotoUri;
                if (!string.IsNullOrWhiteSpace(model.Title) && model.Title != "null")
                {
                    ViewBag.titl = model.Title;
                }
                else
                {
                    ViewBag.titl = "Add Title";
                }
                if (!string.IsNullOrWhiteSpace(model.AuthorDescription) && model.AuthorDescription != "null")
                {
                    ViewBag.comm = model.AuthorDescription;
                }
                else
                {
                    ViewBag.comm = "Add Description";
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
    }
}