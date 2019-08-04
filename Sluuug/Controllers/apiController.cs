using Context;
using Slug.Context;
using Slug.Context.Attributes;
using Slug.Context.Dto.Search;
using Slug.Context.Dto.Settings;
using Slug.Context.Dto.VideoConference;
using Slug.Helpers;
using Slug.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using System.Web.Mvc;
using Slug.Helpers.BaseController;
using Slug.Model.Users;
using Slug.Context.Dto.UserFullInfo;
using Slug.Model.Albums;
using Slug.Context.Dto.UserWorker;
using Slug.Context.Dto.Albums;
using Slug.Context.Dto.Fotos;
using Slug.Context.Dto;

namespace Slug.Controllers
{
    [AuthSlug]
    public class apiController : SlugController
    {

        [HttpPost]
        public JsonResult get_user_info()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var UserInfo = UsersHandler.GetCurrentProfileInfo(sessionId);
            var result = new JsonResult();
            result.Data = UserInfo;
            return result;
        }

        [HttpPost]
        public JsonResult get_info_other_user(int id)
        {
            var UserInfo = UsersHandler.GetUserInfo(id);
            var result = new JsonResult();
            result.Data = UserInfo;
            return result;
        }

        [HttpPost]
        public JsonResult user_vc_role(string converenceID)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var VCWorker = new VideoConferenceHandler();
            var role = VCWorker.UserVCType(sessionId, Guid.Parse(converenceID));
            if (role == VideoConverenceCallType.Caller)
                return new JsonResult() { Data = new Role { type = "CALLER" } };
            if (role == VideoConverenceCallType.Calle)
                return new JsonResult() { Data = new Role { type = "CALLE" } };

            return null;
        }

        [HttpPost]
        public JsonResult save_settings(SetSettingsRequest newSettings)
        {
            var settingsHandler = new SettingsHandler();

            var result = settingsHandler.Change(Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value, newSettings);
            return new JsonResult()
            {
                Data = new SetSettingsResponse()
                {
                    Comment = result
                }
            };            
        }

        [HttpPost]
        public JsonResult cities(int countryCode)
        {
            var model = new SearchCitiesModel();

            var handler = new SearchHandler();
            model.Cities = handler.CitiesByCountryID(countryCode);
            return new JsonResult() { Data = model };
        }

        [HttpPost]
        public JsonResult drop_entry(Guid EntryId)
        {
            string session = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            int userSessionId = UsersHandler.UserIdBySession(session);
            int userEntryID = FullInfoHandler.GetUserByInfoEnrtuGuid(EntryId).Id;
            if (userSessionId == userEntryID)
            {
                FullInfoHandler.DropEntryByGuid(EntryId);
                return new JsonResult() { Data = true };
            }
            return new JsonResult() { Data = false };
        }

        [HttpPost]
        public JsonResult add_education(EducationModel model)
        {
            bool resultFlag = FullInfoHandler.AddEducationEntry(model, Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value);
            return new JsonResult() { Data = resultFlag };
        }

        [HttpPost]
        public JsonResult add_event(MemorableEventsModel model, IEnumerable<HttpPostedFileBase> uploadPhotos)
        {
            string session = Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            bool resultFlag = FullInfoHandler.AddMemEventEntry(model, session, uploadPhotos);
            return new JsonResult() { Data = resultFlag };
        }

        [HttpPost]
        public JsonResult add_works(WorkPlacesModel model)
        {
            bool resultFlag = FullInfoHandler.AddWorkPlacesEntry(model, Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value);
            return new JsonResult() { Data = resultFlag };
        }

        [HttpPost]
        public JsonResult create_album(AlbumModel model, HttpPostedFileBase album_label)
        {
            var createResult = AlbumsHandler.CreateAlbum(GetCookiesValue(Request), model, album_label);
            return new JsonResult() { Data = createResult.isSuccess };
        }

        [HttpPost]
        public JsonResult upload_foto(string albumId, IEnumerable<HttpPostedFileBase> files)
        {
            var Id = Guid.Parse(albumId);
            var uploadResult = AlbumsHandler.UploadToAlbum(GetCookiesValue(Request), Id, files);
            return new JsonResult() { Data = uploadResult.isSuccess };
        }
        
        [HttpPost]
        public JsonResult fotos(Guid album)
        {
            AlbumPhotosResponse result = AlbumsHandler.ExpandAlbum(GetCookiesValue(Request), album);
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult edit_photo(EditFotoInfoModel model)
        {
            var result = AlbumsHandler.EditFotoInfo(GetCookiesValue(Request), model);
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult drop_foto(string fotoId)
        {
            var guid = Guid.Parse(fotoId);
            var result = AlbumsHandler.DropFoto(GetCookiesValue(Request), guid);
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult drop_album(string albumId)
        {
            Guid guid = Guid.Parse(albumId);
            var result = AlbumsHandler.DropAlbum(GetCookiesValue(Request), guid);
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult get_photo_expand(string fotoId)
        {
            Guid guid = Guid.Parse(fotoId);
            FotoCommentsResponse result = AlbumsHandler.GetCommentsToFoto(GetCookiesValue(Request), guid);
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult post_comments(PostCommentToFoto model)
        {
            var result = AlbumsHandler.PostNewComments(GetCookiesValue(Request), model);
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public JsonResult edit_profile(UserParams paramNumer, string newValue)
        {
            var resp = UsersHandler.ChangeParameter(GetCookiesValue(Request), paramNumer, newValue);
            return new JsonResult { Data = resp };
        }
    }
}