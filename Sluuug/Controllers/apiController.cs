using Context;
using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Slug.Helpers;
using Slug.Model.Users;
using Slug.Model.Albums;
using Slug.Context.Dto;
using Slug.Context.Dto.Fotos;
using Slug.Context.Dto.Albums;
using Slug.Context.Attributes;
using Slug.Context.Dto.Search;
using Slug.Context.Dto.Settings;
using Slug.Helpers.BaseController;
using Slug.Context.Dto.VideoConference;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Newtonsoft.Json;
using Slug.Resources.emoji;
using Slug.Helpers.Handlers.HandlersInterface;
using System.Threading.Tasks;
using Slug.Context.Dto.UserWorker_refactor;
using Slug.Context.Tables;
using Slug.Context.Dto.News;
using Slug.Helpers.Handlers.OAuthHandlers;
using Slug.Context.Dto.Posts;
using Slug.Helpers.Handlers.PrivateUserServices;
using Slug.Context.Dto.Conversation;
using Slug.Context.Dto.CryptoConversation;

namespace Slug.Controllers
{
    [AuthSlug]
    public class apiController : SlugController
    {

        [HttpPost]
        public JsonResult get_user_info()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var UserInfo = UsersHandler.ProfileInfo(sessionId);
            var result = new JsonResult();
            result.Data = UserInfo;
            return result;
        }

        [HttpPost]
        public JsonResult get_info_other_user(int id)
        {
            var UserInfo = UsersHandler.BaseUser(id);
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
            return new JsonResult() { Data = result };
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
            ExpandedAlbumModel result = AlbumsHandler.ExpandAlbum(GetCookiesValue(Request), album);
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
        public JsonResult edit_profile(UserParams paramNumer, string newValue, string additionParameter = null)
        {
            var resp = UsersHandler.ChangeParameter(GetCookiesValue(Request), paramNumer, newValue, additionParameter);
            return new JsonResult { Data = resp };
        }

        [HttpPost]
        public JsonResult emoji()
        {
            string file = System.IO.File.ReadAllText(Server.MapPath("~/Resources/emoji/emojis.json"));
            var model = JsonConvert.DeserializeObject<EmojiModel>(file);
            return new JsonResult()
            {
                Data = model.faces
            };
        }

        [HttpPost]
        public async Task<bool> delete_dialog(string ConversationId)
        {
            Guid guid = Guid.Parse(ConversationId);
            string session = GetCookiesValue(Request);
            var disableHandler = new DisableConversationHandler();
            try
            {
                BaseUser user = UsersHandler.BaseUser(session);
                if (user != null)
                {
                    using (var context = new DataBaseContext())
                    {
                        bool dialogBelongUserFlag = await disableHandler.IsDialogBelongUser(context, guid, user.UserId);
                        if (dialogBelongUserFlag)
                        {
                            await disableHandler.DisableDialog(context, guid, user.UserId);
                            return true;
                        }
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        public async Task block_user(BlockContactRequest request)
        {
            string session = GetCookiesValue(Request);
            var user = UsersHandler.BaseUser(session);
            if (user != null)
            {
               UsersHandler.DropFrienship(session, request.BlockedUserId);
               await UsersHandler.BlockUser(user.UserId, request);
            }
        }

        [HttpPost]
        public async Task unblockuser(UnblockContactRequest request)
        {
            string session = GetCookiesValue(Request);
            var user = UsersHandler.BaseUser(session);
            if (user != null)
            {
                await UsersHandler.UnblockUser(user.UserId, request);
            }
        }

        [HttpPost]
        public JsonResult not_readed()
        {
            string session = GetCookiesValue(Request);
            int userId = UsersHandler.UserIdBySession(session);
            NotShowedNews resp = ConversationHandler.News(userId);
            if (resp != null)
            {
                return new JsonResult()
                {
                    Data = resp
                };
            }
            else return null;
        }

        [HttpPost]
        public bool is_online(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return UsersHandler.IsOnline(int.Parse(userId));
            }
            else return false;
        }

        [HttpPost]
        public bool create_post(NewPostRequestModel post)
        {
            string session = GetCookiesValue(Request);
            int userId = UsersHandler.UserIdBySession(session);
            bool saved = PostUserHandler.SavePost(post, userId);
            return saved;
        }

        [HttpPost]
        public JsonResult getmoreownposts(int currentPosts)
        {
            string session = GetCookiesValue(Request);
            int userId = UsersHandler.UserIdBySession(session);
            ProfilePostModel oldPosts = PostUserHandler.GetMorePosts(userId, currentPosts);
            return new JsonResult() { Data = oldPosts };
        }

        [HttpPost]
        public JsonResult getmoreuserposts(int currentUserPostsCount, int userPostNeedId)
        {
            string session = GetCookiesValue(Request);
            int userRequesterId = UsersHandler.UserIdBySession(session);
            if (FriendshipChecker.CheckUsersFriendshipByIDs(userPostNeedId, userRequesterId))
            {
                var oldPosts = PostUserHandler.GetMorePosts(userPostNeedId, currentUserPostsCount);
                return new JsonResult() { Data = oldPosts };
            }
            return null;
        }

        [HttpDelete]
        public JsonResult deletepost(Guid postId)
        {
            return null;
        }

        [HttpPost]
        public JsonResult getvips()
        {
            string session = GetCookiesValue(Request);
            int userId = UsersHandler.UserIdBySession(session);
            UserLocation location = UsersHandler.GetUserLocation(userId);
            var vipsList = Vip.GetVipsByCity(location);
            return new JsonResult() { Data = vipsList };
        }

        [HttpPost]
        public JsonResult getmoreusers(SearchMoreUserRequest request)
        {
            SearchUsersResponse resp = SearchHandler.SearchMoreUsers(request);
            return new JsonResult() { Data = resp };
        }

        [HttpPost]
        public JsonResult getmoremessages(MoreMessagesDialogRequest request)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            bool verifyConvers = UsersHandler.CheckConversationBySessionId(sessionId, request.DialogId);
            if (verifyConvers)
            {
                int requestSenderUserId = UsersHandler.UserIdBySession(sessionId);
                MoreMessegesDialogResponce responce = DialogsHandler.GetMoreMessages(request, requestSenderUserId);
                return new JsonResult() { Data = responce };
            }
            return null;
        }

        [HttpPost]
        public JsonResult getmorecryptomessages(MoreMessagesDialogRequest request)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            int requestSenderUserId = UsersHandler.UserIdBySession(sessionId);
            request.UserId = requestSenderUserId;
            MoreCryptoDialogMessagesResponce resp = CryptoChatHandler.GetMoreMessages(request);
            return new JsonResult() { Data = resp };
        }
    }
}