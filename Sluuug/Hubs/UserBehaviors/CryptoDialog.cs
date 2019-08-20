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
using Slug.Context.Tables;
using System.Data.Entity;
using Slug.ImageEdit;
using Slug.Helpers.Handlers.HandlersInterface;
using Slug.Model.Messager.CryptoChat;

namespace Slug.Hubs
{
    public class CryptoDialog : Hub
    {
        public CryptoDialog(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
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

        public async Task<NotificationModel> InviteUsersToCryptoChat(string offer_to_cripto_chat, Guid userInvited)
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

            Clients.Clients(UserRecipientsConnectionIds.ConnectionId).ObtainNewInvitation(cryptoConversation);

            var responce = new NotificationModel();
            responce.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
            responce.PublicDataToExcange = cryptoConversation;
            responce.FromUser = fromUser;
            responce.Culture = UserRecipientsConnectionIds.CultureCode[0];
            return responce;
        }

        public async Task<NotificationModel> AcceptInvite(string ansver_to_cripto_chat)
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

            var responce = new NotificationModel();
            responce.ConnectionIds = connections.ConnectionId;
            responce.PublicDataToExcange = cryptoConversation;
            responce.FromUser = userAccepter;
            responce.Culture = connections.CultureCode[0];
            return responce;
        }

        public async Task<NotificationModel> SendMessage(string message)
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
            Guid guidChatId = Guid.Parse( matches[0].ToString().Substring(1) );
            BaseUser fromUser = userHandler.BaseUser(fromUserID);
            int toUserID = cryptoChatWorker.GetInterlocutorID(guidChatId, fromUser.UserId);
            bool isFriends = FriendshipChecker.CheckUsersFriendshipByIDs(fromUserID, toUserID);

            if (isFriends)
            {
                var dialogDisableHandler = new DisableConversationHandler();

                await cryptoChatWorker.SaveSecretMessageHashAsync(guidChatId, fromUserID, message);
                await dialogDisableHandler.EnableDialog(guidChatId);
                var cryptoDialogModel = cryptoChatWorker.GetCryptoDialogModel(guidChatId);

                UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUserID);
                var messageModel = new CryptoMessageModel()
                {
                     Name = fromUser.Name,
                     SurName = fromUser.SurName,
                     AvatatURI = Resize.ResizedAvatarUri(fromUser.AvatarResizeUri, ModTypes.c_scale, 60, 60),
                     Text = message,
                     DialogId = guidChatId,
                     
                };

                Clients.Clients(UserRecipientsConnectionIds.ConnectionId)
                    .GetCryptoMessage(messageModel,
                    Resize.ResizedAvatarUri(fromUser.AvatarResizeUri, ModTypes.c_scale, 100, 100),
                    cryptoDialogModel.RemainingMins,
                    cryptoDialogModel.RemainingSecs,
                    cryptoDialogModel.CloseDate.ToString("D")
                    );

                var response = new NotificationModel();
                response.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
                response.FromUser = fromUser;
                response.Culture = UserRecipientsConnectionIds.CultureCode[0];
                return response;
            }
            else return null;
        }

        public async Task<NotificationModel> RefuseInvitation(Guid cryptoConversationGuidID)
        {
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            var userHandler = new UsersHandler();
            BaseUser userRefuser = userHandler.BaseUser(session);
            using (var context = new DataBaseContext())
            {
                SecretChatGroup creatordUser = await context.SecretChatGroups
                    .FirstOrDefaultAsync(x => 
                    x.PartyGUID == cryptoConversationGuidID &&
                    x.UserId != userRefuser.UserId);
                if (creatordUser != null)
                {
                    var connectionHandler = new UsersConnectionHandler();
                    UserConnectionIdModel connection = connectionHandler.GetConnectionById(context, creatordUser.UserId);

                    SecretChat chatEntry = await context.SecretChat.FirstOrDefaultAsync(x => x.PartyGUID == cryptoConversationGuidID);
                    List<SecretChatGroup> chatUsersEntrys = await context.SecretChatGroups.Where(x => x.PartyGUID == cryptoConversationGuidID).ToListAsync();

                    context.SecretChat.Remove(chatEntry);
                    context.SecretChatGroups.RemoveRange(chatUsersEntrys);
                    await context.SaveChangesAsync();

                    var response = new NotificationModel();
                    response.ConnectionIds = connection.ConnectionId;
                    response.Culture = connection.CultureCode[0];
                    response.FromUser = userRefuser;
                    return response;
                }
            }
            return null;
        }
    }
}