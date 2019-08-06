using System;
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
using Slug.Model.Users;
using Slug.Helpers.HTMLGenerated;

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
            try
            {
                var Cookie = base.Context.Request.Cookies;
                var session_id = Cookie[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
                var uW = new UsersHandler();
                var UserInfo = uW.ProfileInfo(session_id.Value);

                var CryptoChatResponce = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(create_request);
                int participantID = CryptoChatResponce.Participants[0].UserId;
                bool isFriend = FriendshipChecker.CheckUsersFriendshipByIDs(UserInfo.UserId, participantID);
                if (isFriend)
                {
                    CryptoChatResponce.CreatorName = UserInfo.Name;
                    CryptoChatResponce.CreatorAvatar = UserInfo.AvatarResizeUri;
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
            catch (Exception)
            {
                Clients.Caller.FailCreateNewCryptoConversation(0);
            }
        }

        public async Task<NotifyHubModel> InviteUsersToCryptoChat(string offer_to_cripto_chat, Guid userInvited)
        {
            Cookie Session = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            UsersHandler worker = new UsersHandler();
            BaseUser fromUser = worker.ProfileInfo(Session.Value, false);

            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(offer_to_cripto_chat);

            cryptoConversation.CreatorAvatar = fromUser.AvatarResizeUri;
            cryptoConversation.CreatorName = fromUser.Name;

            var connectionWorker = new UsersConnectionHandler();
            var cryptoChatWorker = new CryptoChatHandler();
            int toUser = cryptoChatWorker.GetInterlocutorID(userInvited, fromUser.UserId);
            UserConnectionIdModel UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUser);

            //string html = InviteToSecretConversation.GenerateHtml(cryptoConversation, UserRecipientsConnectionIds.CultureCode[0]);
            //Clients.Clients(UserRecipientsConnectionIds.ConnectionId).ObtainNewInvitation(cryptoConversation, html);

            var responce = new NotifyHubModel();
            responce.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
            responce.PublicDataToExcange = cryptoConversation;
            responce.FromUser = fromUser;
            responce.Culture = UserRecipientsConnectionIds.CultureCode[0];
            return responce;
        }

        public async Task<NotifyHubModel> AcceptInvite(string ansver_to_cripto_chat)
        {
            var connectionWorker = new UsersConnectionHandler();
            var CrWorker = new CryptoChatHandler();
            var UsWorker = new UsersHandler();

            var cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userAccepter = UsWorker.ProfileInfo(cookies.Value, false);
            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(ansver_to_cripto_chat);


            CrWorker.UpdateAcceptCryptoChat(userAccepter.UserId, cryptoConversation.ConvGuidId);
            int interlocutor = CrWorker.GetInterlocutorID(cryptoConversation.ConvGuidId, userAccepter.UserId);
            UserConnectionIdModel connections = connectionWorker.GetConnectionById(interlocutor);

            Clients.Clients(connections.ConnectionId).AcceptInvitation(ansver_to_cripto_chat);

            var responce = new NotifyHubModel();
            responce.ConnectionIds = connections.ConnectionId;
            responce.PublicDataToExcange = cryptoConversation;
            responce.FromUser = userAccepter;
            responce.Culture = connections.CultureCode[0];
            return responce;
        }

        public async Task<NotifyHubModel> SendMessage(string message)
        {
            var connectionWorker = new UsersConnectionHandler();
            var cryptoChatWorker = new CryptoChatHandler();
            var UserRecipientsConnectionIds = new UserConnectionIdModel();

            UsersHandler userHandler = new UsersHandler();
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            int fromUserID = userHandler.UserIdBySession(cookies.Value);

            string uri = base.Context.QueryString["URL"];
            var reg = new Regex("=.{36}");
            MatchCollection matches = reg.Matches(uri);
            string guidChatId = matches[0].ToString().Substring(1);
            BaseUser fromUser = userHandler.BaseUser(fromUserID);
            int toUserID = cryptoChatWorker.GetInterlocutorID(Guid.Parse(guidChatId), fromUser.UserId);
            bool isFriends = FriendshipChecker.CheckUsersFriendshipByIDs(fromUserID, toUserID);

            if (isFriends)
            {
                await cryptoChatWorker.SaveSecretMessageHashAsync(guidChatId, fromUserID, message);
                UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUserID);

                Clients.Clients(UserRecipientsConnectionIds.ConnectionId).NewMessage(message, fromUser.AvatarResizeUri, fromUser.Name, DateTime.Now, guidChatId);
                Clients.Caller.NewMessage(message, fromUser.AvatarResizeUri, fromUser.Name, DateTime.Now, guidChatId);

                var response = new NotifyHubModel();
                response.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
                response.FromUser = fromUser;
                response.Culture = UserRecipientsConnectionIds.CultureCode[0];
                return response;
            }
            else return null;
        }
    }
}