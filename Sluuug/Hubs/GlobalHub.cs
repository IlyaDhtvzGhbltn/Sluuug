using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context.Dto.Messages;
using Slug.Helpers;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Helpers.BaseController;
using Context;
using Slug.Model.Users;
using Slug.Helpers.HTMLGenerated;
using Slug.Context.Dto.Notification;
using Slug.Model.Users.Relations;
using Slug.Context.Attributes;
using SharedModels.Users;


namespace Slug.Hubs
{
    [HubName("MainGlobalHub")]
    [AuthSlug]
    public class GlobalHub : Hub
    {

        public async Task UpdateConnection()
        {
            await Task.Run(() => 
            {
                string connectionId = Context.ConnectionId;
                Guid connectionGuid = Guid.Parse(connectionId);
                using (var context = new DataBaseContext())
                {
                    var connectionEntry = context.UserConnections.FirstOrDefault(x => x.ConnectionId == connectionGuid);
                    if (connectionEntry != null)
                    {
                        connectionEntry.UpdateTime = DateTime.UtcNow;
                        connectionEntry.IsActive = true;
                        context.SaveChanges();
                    }
                }
            });
        }
        public async Task OpenConnect()
        {
            var connectionHandler = new UsersConnectionHandler();
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            string connection = Context.ConnectionId;
            object tempObject;
            Context.Request.Environment.TryGetValue("server.RemoteIpAddress", out tempObject);
            string ipAddress = (string)tempObject;
            //Logger logger = LogManager.GetCurrentClassLogger();
            //logger.Debug(ipAddress);
            await connectionHandler.AddConnection(connection, session, ipAddress);
        }
        public void CloseConnect()
        {
            var connectionHandler = new UsersConnectionHandler();
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            string closedConnection = Context.ConnectionId;
            connectionHandler.CloseConnection(session, closedConnection);
        }





        public async Task SendMessage(string message, string convId, int toUserId)
        {
            var userHandler = new UsersHandler();
            var vipHandler = new VipUsersHandler();
            var dialog = new UsersDialogHandler();
            string session = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;

            int userSenderId = userHandler.UserIdBySession(session);
            toUserId = convId != "0" ? dialog.GetConversatorsIds(Guid.Parse(convId)).First(x => x != userSenderId) : toUserId;


            bool isFriends = FriendshipChecker.CheckUsersFriendshipByIDs(userSenderId, toUserId);
            bool isAvaliableContact = vipHandler.SenderAvaliableContact(userSenderId, toUserId);

            if (isFriends && isAvaliableContact)
            {
                var messageHub = new SimpleDialog(base.Context, base.Clients);
                NotificationModel hubResp = await messageHub.SendMessage(message, convId, toUserId);
                await Clients.Caller.MessageSendedResult(true, "Ваше сообщение было отправлено!");

                if (hubResp != null)
                {
                    if (hubResp.ConnectionIds.Count > 0)
                    {
                        string html = Notifications.GenerateHtml(NotificationType.NewMessage, hubResp.FromUser, hubResp.Culture);
                        await Clients.Clients(hubResp.ConnectionIds).NotifyAbout(html, null, NotificationType.NewMessage, convId);
                    }
                }
            }
            if(!isFriends)
                await Clients.Caller.MessageSendedResult(false, "Вы не можете отправить сообщение пользователю, пока он не добавит вас в свои контакты.");
            if (!isAvaliableContact)
                await Clients.Caller.MessageSendedResult(false, "Вы не можете отправить сообщение VIP пользователю. Активируйте VIP для того что бы продолжить.");
        }

        public async Task SendCutMessage(string message, Guid convID)
        {
            Cookie cookies = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];

            using (var context = new DataBaseContext())
            {
                var conversation = context.Conversations.FirstOrDefault(x => x.ConversationGuidId == convID);
                if (conversation != null)
                {
                    int[] participants = context.ConversationGroup
                        .Where(x => x.ConversationGuidId == convID)
                        .Select(x => x.UserId).ToArray();
                    if (participants.Length >= 2)
                    {
                        var UsWork = new UsersHandler();
                        ProfileModel fromUser = UsWork.ProfileInfo(cookies.Value);

                        if (participants.Contains(fromUser.UserId))
                        {
                            int toUserID = participants.Where(x => x != fromUser.UserId).First();
                            bool isFriends = FriendshipChecker.CheckUsersFriendshipByIDs(fromUser.UserId, toUserID);
                            if (isFriends)
                            {
                                bool fromUserQuickSett = context.Users.Where(x => x.Id == fromUser.UserId).First().Settings.QuickMessage;
                                bool toUserQuickSett = context.Users.Where(x => x.Id == toUserID).First().Settings.QuickMessage;

                                if (fromUserQuickSett && toUserQuickSett)
                                {
                                    var connectionHandler = new UsersConnectionHandler();
                                    UserConnectionIdModel UserRecipientsConnectionIds = connectionHandler.GetConnectionById(toUserID);
                                    Clients.Clients(UserRecipientsConnectionIds.ConnectionId).getCutMessage(message);
                                }
                            }
                        }
                    }
                }
            }
        }





        public async Task CreateNewCryptoConversation(string create_request)
        {
            var cryptoHub = new CryptoDialog(base.Context, base.Clients);
            cryptoHub.CreateNewCryptoConversation(create_request);
        }

        public async Task InviteUsersToCryptoChat(string offerToCriptoChat, Guid cryptoConversationGuidID)
        {
            var cryptoHub = new CryptoDialog(base.Context, base.Clients);
            NotificationModel response = await cryptoHub.InviteUsersToCryptoChat(offerToCriptoChat, cryptoConversationGuidID);
            string html = Notifications.GenerateHtml(NotificationType.NewInviteSecretChat, response.FromUser, response.Culture);
            Clients.Clients(response.ConnectionIds).NotifyAbout(html, response.PublicDataToExcange, NotificationType.NewInviteSecretChat);
        }

        public async Task AcceptInvite(string ansver_to_cripto_chat)
        {
            var cryptoHub = new CryptoDialog(base.Context, base.Clients);
            NotificationModel response = await cryptoHub.AcceptInvite(ansver_to_cripto_chat);
            string html = Notifications.GenerateHtml(NotificationType.AcceptYourInviteSecretChat, response.FromUser, response.Culture );
            Clients.Clients(response.ConnectionIds).NotifyAbout(html, response.PublicDataToExcange, NotificationType.AcceptYourInviteSecretChat);
        }

        public async Task SendMessage(string message)
        {
            var cryptoHub = new CryptoDialog(base.Context, base.Clients);
            var response = await cryptoHub.SendMessage(message);
            if (response != null)
            {
                string html = Notifications.GenerateHtml(NotificationType.NewMessageSecret, response.FromUser, response.Culture);
                Clients.Clients(response.ConnectionIds).NotifyAbout(html, null, NotificationType.NewMessageSecret, response.DialogId);
            }
            else
            {
               await Clients.Caller.MessageSendedResult(false);
            }
        }

        public async Task RefuseCryptoChatInvitation(Guid cryptoConversationGuidID)
        {
            var cryptoHub = new CryptoDialog(base.Context, base.Clients);
            NotificationModel response = await cryptoHub.RefuseInvitation(cryptoConversationGuidID);
            if (response != null)
            {
                string html = Notifications.GenerateHtml(NotificationType.CryptoInviteRefuse, response.FromUser, "ru-RU");
                Clients.Clients(response.ConnectionIds).NotifyAbout(html, null, NotificationType.CryptoInviteRefuse);
            }
        }




        public async Task CreateAndInvite(int calleUserId)
        {
            var videoHub = new VideoConference(base.Context, base.Clients);
            NotificationModel responce = await videoHub.CreateAndInvite(calleUserId);
            if (responce != null && responce.ConnectionIds.Count > 0)
            {
                string html = Notifications.GenerateHtml(NotificationType.NewInviteVideoConference, responce.FromUser, responce.Culture);
                Clients.Clients(responce.ConnectionIds).NotifyAbout(html, null, NotificationType.NewInviteVideoConference);
            }
        }

        public async Task Invite(string callOffer, Guid videoConverenceGuidID)
        {
            var videoHub = new VideoConference(base.Context, base.Clients);
            videoHub.Invite(callOffer, videoConverenceGuidID);
        }

        public async Task ConfirmInvite(Guid guid, string callAnswer)
        {
            var videoHub = new VideoConference(base.Context, base.Clients);
            videoHub.ConfirmInvite(guid, callAnswer);
        }

        public async Task ExchangeICandidates(dynamic iceCandidate, Guid guidID)
        {
            var videoHub = new VideoConference(base.Context, base.Clients);
            videoHub.ExchangeICandidates(iceCandidate, guidID);
        }

        public void GetVideoParticipantName(Guid ID)
        {
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            var conferenceHandler = new VideoConferenceHandler();
            var userHandler = new UsersHandler();

            int[] IDs = conferenceHandler.GetVideoConferenceParticipantsIDs(ID);
            int myId = userHandler.UserIdBySession(session);
            int participantID = IDs.First(x => x != myId);
            var participantInfo = userHandler.BaseUser(participantID);
            string participantName = string.Format("{0} {1}", participantInfo.Name, participantInfo.SurName);
            Clients.Caller.SendName(participantName);
        }

        public async Task CloseVideoConverence(Guid guidID)
        {
            var videoHub = new VideoConference(base.Context, base.Clients);
            videoHub.CloseVideoConverence(guidID);
        }




        public async Task AddFriend(int userID)
        {
            var userWorker = new UsersHandler();
            var connectionsWorker = new UsersConnectionHandler();

            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            BaseUser responce = userWorker.AddInviteToContacts(session, userID);
            UserConnectionIdModel connections = connectionsWorker.GetConnectionById(userID);
            if (responce != null)
            {
                string html = Notifications.GenerateHtml(NotificationType.NewInviteFriendship, responce, connections.CultureCode[0]);
                Clients.Clients(connections.ConnectionId).NotifyAbout(html, null, NotificationType.NewInviteFriendship);
            }
            //string htmlToCaller = UserFriendshipResponce.GenerateHtml(connections.CultureCode[0]);
            //Clients.Caller.AddUserResponce(htmlToCaller);
        }

        public async Task DropContact(int userID)
        {
            var userWorker = new UsersHandler();
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;

            userWorker.DropFrienship(session, userID);
        }

        public async Task AcceptContact(int userID)
        {
            var userWorker = new UsersHandler();
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;

            NotificationModel response = await userWorker.AcceptInviteToContacts(session, userID);
            string html = Notifications.GenerateHtml(NotificationType.AcceptFriendship, response.FromUser, response.Culture);
            Clients.Clients(response.ConnectionIds).NotifyAbout(html, null, NotificationType.AcceptFriendship);
        }

    }
}