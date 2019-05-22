﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using Slug.Context;
using Slug.Helpers;
using Newtonsoft.Json;
using Slug.Model;
using Slug.Context.Dto.CryptoConversation;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context.Dto.Messages;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Helpers.BaseController;

namespace Slug.Hubs
{
    public class CryptoMessagersHub : Hub
    {
        public CryptoMessagersHub(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        public void CreateNewCryptoConversation(string create_request)
        {
            var Cookie = base.Context.Request.Cookies;
            var session_id = Cookie[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            var uW = new UsersHandler();
            var UserInfo = uW.GetFullUserInfo(session_id.Value);

            var CryptoChatResponce = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(create_request);
            int participantID = CryptoChatResponce.Participants[0].UserId;
            bool isFriend = FriendshipChecker.CheckUsersFriendshipByIDs(UserInfo.UserId, participantID);
            if (isFriend)
            {
                CryptoChatResponce.CreatorName = UserInfo.Name;
                CryptoChatResponce.CreatorAvatar = UserInfo.AvatarUri;
                CryptoChatResponce.CreationDate = DateTime.Now;

                var CryptWorker = new CryptoChatHandler();
                var response = CryptWorker.CreateNewCryptoChat(CryptoChatResponce.Type, CryptoChatResponce.Participants, UserInfo.UserId);
                CryptoChatResponce.ConvGuidId = response.CryptoGuidId;
                CryptoChatResponce.CreatorUserId = UserInfo.UserId;
                CryptoChatResponce.Participants.Add(
                    new Participant()
                    {
                        UserId = CryptoChatResponce.CreatorUserId
                    });
                CryptoChatResponce.ExpireDate = response.ExpireDate;

                Clients.Caller.NewCryptoConversationCreated(CryptoChatResponce);
            }
        }

        public async Task<PartialHubResponse> InviteUsersToCryptoChat(string offer_to_cripto_chat, Guid userInvited)
        {
            Cookie Session = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            UsersHandler worker = new UsersHandler();
            CutUserInfoModel fromUser = worker.GetFullUserInfo(Session.Value);

            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(offer_to_cripto_chat);
            cryptoConversation.CreatorAvatar = fromUser.AvatarUri;
            cryptoConversation.CreatorName = fromUser.Name;

            var connectionWorker = new UsersConnectionHandler();
            var cryptoChatWorker = new CryptoChatHandler();
            int toUser = cryptoChatWorker.GetInterlocutorID(userInvited, fromUser.UserId);
            IList<string> UserRecipientsConnectionIds = new List<string>();
            UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUser);

            Clients.Clients(UserRecipientsConnectionIds).ObtainNewInvitation(cryptoConversation);

            var responce = new PartialHubResponse();
            responce.ConnectionIds = UserRecipientsConnectionIds;
            responce.PublicDataToExcange = cryptoConversation;
            responce.FromUser = fromUser;
            return responce;
        }

        public async Task<PartialHubResponse> AcceptInvite(string ansver_to_cripto_chat)
        {
            var connectionWorker = new UsersConnectionHandler();
            var CrWorker = new CryptoChatHandler();
            var UsWorker = new UsersHandler();

            var cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            CutUserInfoModel userAccepter = UsWorker.GetFullUserInfo(cookies.Value);
            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(ansver_to_cripto_chat);


            CrWorker.UpdateAcceptCryptoChat(userAccepter.UserId, cryptoConversation.ConvGuidId);
            int interlocutor = CrWorker.GetInterlocutorID(cryptoConversation.ConvGuidId, userAccepter.UserId);
            IList<string> connections = connectionWorker.GetConnectionById(interlocutor);

            Clients.Clients(connections).AcceptInvitation(ansver_to_cripto_chat);

            var responce = new PartialHubResponse();
            responce.ConnectionIds = connections;
            responce.PublicDataToExcange = cryptoConversation;
            responce.FromUser = userAccepter;
            return responce;
        }

        public async Task<PartialHubResponse> SendMessage(string message)
        {
            var connectionWorker = new UsersConnectionHandler();
            var cryptoChatWorker = new CryptoChatHandler();
            IList<string> UserRecipientsConnectionIds = new List<string>();

            UsersHandler UsWorker = new UsersHandler();
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            int fromUserID = UsWorker.GetFullUserInfo(cookies.Value).UserId;

            string uri = base.Context.QueryString["URL"];
            var reg = new Regex("=.{36}");
            MatchCollection matches = reg.Matches(uri);
            string guidChatId = matches[0].ToString().Substring(1);
            CutUserInfoModel fromUser = UsWorker.GetUserInfo(fromUserID);
            int toUserID = cryptoChatWorker.GetInterlocutorID(Guid.Parse(guidChatId), fromUser.UserId);
            bool isFriends = FriendshipChecker.CheckUsersFriendshipByIDs(fromUserID, toUserID);

            if (isFriends)
            {
                await cryptoChatWorker.SaveSecretMessageHashAsync(guidChatId, fromUserID, message);
                UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUserID);

                Clients.Clients(UserRecipientsConnectionIds).NewMessage(message, fromUser.AvatarUri, fromUser.Name, DateTime.Now, guidChatId);
                Clients.Caller.NewMessage(message, fromUser.AvatarUri, fromUser.Name, DateTime.Now, guidChatId);

                var response = new PartialHubResponse();
                response.ConnectionIds = UserRecipientsConnectionIds;
                response.FromUser = fromUser;
                return response;
            }
            else return null;
        }
    }
}