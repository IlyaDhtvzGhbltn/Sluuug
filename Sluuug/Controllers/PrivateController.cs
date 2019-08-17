﻿using Context;
using Slug.Context.Attributes;
using Slug.Helpers;
using Slug.Model;
using Slug.Model.Users;
using Slug.Model.VideoConference;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Slug.Context.Dto.UserWorker;
using Newtonsoft.Json;
using Slug.Context.Dto.Search;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Helpers.BaseController;
using Slug.Context.Dto.Cloudinary;
using Slug.Model.Users.Relations;
using Slug.DbInitialisation;
using System.Collections.Generic;
using Slug.ImageEdit;

namespace Slug.Controllers
{
    [AuthSlug]
    public class PrivateController : SlugController
    {
        [HttpGet]
        public async Task<ActionResult> index()
        {
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> my()
        {
            var cookies = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]);
            ProfileModel userInfoModel = UsersHandler.ProfileInfo(cookies.Value);

            string selectedDatingPurposeValue = ((int)userInfoModel.purpose).ToString();
            var datingPurposeList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Познакомлюсь для отношений", Value = "0" },
                new SelectListItem(){ Text = "Познакомлюсь для общения", Value = "1" },
                new SelectListItem(){ Text = "Познакомлюсь для секса", Value = "2" },
                new SelectListItem(){ Text = "Не знакомлюсь", Value = "3" }
            };
            datingPurposeList.ForEach(
                x => {
                    if (x.Value == selectedDatingPurposeValue)
                        x.Selected = true;
                    });
            ViewBag.DatingPurposeList = datingPurposeList;

            string selectedDatingSexValue = ((int)userInfoModel.userSearchSex).ToString();
            var datingSexList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Женщиной", Value = "0" },
                new SelectListItem(){ Text = "Мужчиной", Value = "1" },
            };
            datingSexList.ForEach(
                x => 
                {
                    if (x.Value == selectedDatingSexValue)
                        x.Selected = true;
                });
            ViewBag.DatingSexList = datingSexList;

            string selectedDatingAgeValue = ((int)userInfoModel.userSearchAge).ToString();
            var datingAgesList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "16-20", Value = "0" },
                new SelectListItem(){ Text = "21-26", Value = "1" },
                new SelectListItem(){ Text = "27-31", Value = "2" },
                new SelectListItem(){ Text = "33-40", Value = "3" },
                new SelectListItem(){ Text = "41-49", Value = "4" },
                new SelectListItem(){ Text = "50-59", Value = "5" },
                new SelectListItem(){ Text = "60-69", Value = "6" },
                new SelectListItem(){ Text = "более 70", Value = "7" },
            };
            datingAgesList.ForEach(
                x => 
            {
                if (x.Value == selectedDatingAgeValue)
                    x.Selected = true;
            });
            ViewBag.DatingAgesList = datingAgesList;

            var CountriesList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Россия", Value = "7" },
                new SelectListItem(){ Text = "USA", Value = "1" }
            };
            CountriesList.ForEach(x => 
            {
                if (x.Value == userInfoModel.CountryCode.ToString())
                    x.Selected = true;
            });
            ViewBag.Countries = CountriesList;


            var handler = new SearchHandler();
            var CitiesList = new List<SelectListItem>();
            List<SearchCityItem> cities = handler.CitiesByCountryID(userInfoModel.CountryCode);
            cities.ForEach(x => 
            {
                var listItem = new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.CityCode.ToString()
                };
                if(listItem.Value == userInfoModel.CityCode.ToString())
                    listItem.Selected = true;
                CitiesList.Add(listItem);
            });
            ViewBag.Cities = CitiesList;
            return View(userInfoModel);
        }

        [HttpPost]
        public async Task<ActionResult> upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                CloudImageUploadResult result = UploadImg(upload);
                string cookies = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
                UsersHandler.ChangeAvatarResizeUri(cookies, result.SecureUrl);
            }
            return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> cnv()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            var user = UsersHandler.ProfileInfo(sessionId);
            ConversationGroupModel Conversations = base.ConversationHandler.GetPreConversations(user.UserId);
            return View(Conversations);
        }

        [HttpGet]
        public async Task<ActionResult> msg(Guid id, int page = 1)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            bool verifyConvers = UsersHandler.CheckConversationBySessionId(sessionId, id);
            if (verifyConvers)
            {
                int userId = UsersHandler.UserIdBySession(sessionId);
                DialogModel dialog = DialogsHandler.GetMessanges(id, userId, page);
                dialog.DialogId = id;
                ViewBag.OwnAvatar = dialog.OwnResizeAvatar;
                return View(dialog);
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> user(int id)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;

            var friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(sessionId, id);
            int ownId = UsersHandler.UserIdBySession(sessionId);
            if (ownId != id)
            {
                if (friends)
                {
                    return RedirectToAction("friend", "private", new { id = id });
                }
                else
                {
                    BaseUser model = UsersHandler.GetForeignUserInfo(sessionId, id);
                    if(model == null)
                        return RedirectToAction("my", "private");
                    else
                        return View(model);
                }
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> friend(int id)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            int ownId = UsersHandler.UserIdBySession(sessionId);

            if (ownId != id)
            {
                bool friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(sessionId, id);
                if (friends)
                {
                    
                    ViewBag.MyAvatar = Resize.ResizedAvatarUri(UsersHandler.BaseUser(sessionId).AvatarResizeUri, ModTypes.c_scale, 55, 55);
                    ProfileModel userInfo = UsersHandler.ProfileInfo(id);
                    return View(userInfo);
                }
                else
                    return RedirectToAction("user", "private", new { id = id});
            }
            else
                return RedirectToAction("my", "private");
        }

        [HttpGet]
        public async Task<ActionResult> contacts(string type)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            ContactsModel model = base.UsersHandler.GetContactsBySession(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> invite_video_conversation(string type)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            VideoConferenceModel model = base.VideoConferenceHandler.VideoConferenceModel(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> v_conversation(Guid id)
        {
            bool isActive = base.VideoConferenceHandler.IsConverenceActive(id);
            if (isActive)
                return View();
            else
                return RedirectToAction("invite_video_conversation", "private");
        }

        [HttpGet]
        public async Task<ActionResult> crypto_cnv(string type)
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            CryptoConversationGroupModel model = CryptoChatHandler.GetCryptoChat(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> c_msg(string id, int page = 1)
        {
            string session = GetCookiesValue(this.Request);
            try
            {
                CryptoDialogModel model = await CryptoChatHandler.GetCryptoDialogs(session, Guid.Parse(id), page);
                if (model != null)
                {
                    string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
                    BaseUser user = UsersHandler.BaseUser(sessionId);
                    ViewBag.MyAvatar = Resize.ResizedAvatarUri(user.AvatarResizeUri, ModTypes.c_scale, 60, 60);
                    return View(model);
                }
                else return RedirectToAction("my", "private");
            }
            catch (Exception )
            {
                return RedirectToAction("crypto_cnv", "private");
            }
        }

        [HttpGet]
        public async Task<ActionResult> logout()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            SessionTypes type = SessionHandler.GetSessionType(sessionId);
            if (type == SessionTypes.Private)
            {
                SessionHandler.CloseSession(sessionId);
            }
            return RedirectToAction("index", "guest");
        }

        [HttpGet]
        public async Task<ActionResult> settings()
        {
            string sessionId = Request.Cookies.Get(WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]).Value;
            UserSettingsModel model = UsersHandler.GetSettings(sessionId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> search()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> search_result(
            string user_name, 
            int user_country = -1, 
            int user_city = -1, 
            int user_sex = -1, 
            int user_age = -1, 
            int user_purpose = -1, 
            int user_search_sex = -1, 
            int user_search_age = -1,
            int page = 1)
        {
            var parseRequest = new SearchUsersRequest()
            {
                userCountry = user_country,
                userName = user_name,
                userCity = user_city,
                userAge = user_age,
                userSex = user_sex,
                userDatingPurpose = user_purpose,
                userSearchAge = user_search_age,
                userSearchSex = user_search_sex
            };
            SearchUsersResponse response = SearchHandler.SearchUsers(parseRequest, page);
            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> support()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<ActionResult> notification_history()
        //{
        //    return View();
        //}
    }
}