using Slug.Context.Dto.Albums;
using Slug.Context.Tables;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slug.Controllers
{
    public class partialController : Controller
    {
        public ActionResult OwnAlbum()
        {
            return View("~/Views/Partial/Albums/CreateNew.cshtml");
        }

        [HttpPost]
        public ActionResult ExpandFoto(Guid fotoID, Uri fullFoto, string titl, string comm)
        {
            ViewBag.fotoId = fotoID;
            ViewBag.fullFoto = fullFoto;
            if (!string.IsNullOrWhiteSpace(titl) && titl!="null")
            {
                ViewBag.titl = titl;
            }
            else
            {
                ViewBag.titl = "Add Title";
            }
            if (!string.IsNullOrWhiteSpace(comm) && comm != "null")
            {
                ViewBag.comm = comm;
            }
            else
            {
                ViewBag.comm = "Add Description";
            }
            return View("~/Views/Partial/Albums/Expand.cshtml");
        }

        [HttpPost]
        public ActionResult UploadFotoForm(Guid albumID)
        {
            ViewBag.album = albumID;
            return View("~/Views/Partial/Albums/UploadFotoForm.cshtml");
        }

        [HttpPost]
        public ActionResult AlbumReview(FotoModel album)
        {
            return View("~/Views/Partial/Albums/AlbumReview.cshtml", album);
        }

        //[HttpPost]
        //public ActionResult FriendAlbumReview(FotoModel album)
        //{
        //    return View("~/Views/Partial/Albums/FriendAlbumReview.cshtml", album);
        //}

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
    }
}