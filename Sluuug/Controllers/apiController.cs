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

namespace Slug.Controllers
{
    [AuthSlug]
    public class apiController : SlugController
    {

        [HttpPost]
        public JsonResult get_user_info()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var UserInfo = UsersHandler.GetUserInfo(sessionId);
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
        public JsonResult users(SearchUsersRequest request, int page = 1)
        {
            var handler = new SearchHandler();

            SearchUsersResponse responce = handler.SearchUsers(request, 0, page);
            return new JsonResult() { };
        }
    }
}