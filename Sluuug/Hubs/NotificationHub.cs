using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context;
using Slug.Context.Dto.Messages;
using Slug.Helpers;
using Slug.Model;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Sluuug.Hubs;
using Slug.Helpers.BaseController;
using Context;
using Slug.Model.Users;
using NLog;
using Slug.Helpers.HTMLGenerated;
using Slug.Context.Dto.Notification;
using Slug.Model.Users.Relations;

namespace Slug.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        public void OpenConnect()
        {
            var UCW = new UsersConnectionHandler();
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            string connection = Context.ConnectionId;
            object tempObject;
            Context.Request.Environment.TryGetValue("server.RemoteIpAddress", out tempObject);
            string ipAddress = (string)tempObject;

            //Logger logger = LogManager.GetCurrentClassLogger();
            //logger.Debug(ipAddress);

            UCW.AddConnection(connection, session, ipAddress);
        }

        public void CloseConnect()
        {
            var UCW = new UsersConnectionHandler();
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            string connection = Context.ConnectionId;
            UCW.CloseConnection(connection, session);
        }

        public void GetVideoParticipantName(Guid ID)
        {
            string session = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
            var conferenceHandler = new VideoConferenceHandler();
            var userHandler = new UsersHandler();

            int[] IDs = conferenceHandler.GetVideoConferenceParticipantsIDs(ID);
            int myID = userHandler.GetCurrentProfileInfo(session).UserId;
            int participantID = IDs.First(x => x != myID);
            var participantInfo = userHandler.GetUserInfo(participantID);
            string participantName = string.Format("{0} {1}", participantInfo.Name, participantInfo.SurName);
            Clients.Caller.SendName(participantName);
        }


        /// <summary>
        /// Send none crypt message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="convId"></param>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        public async Task SendMessage(string message, string convId, int toUserId)
        {
            Cookie cookies = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];

            bool isFriends = true;
            if (convId == "0")
            {
                isFriends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(cookies.Value, toUserId);
            }
            else
            {
                isFriends = FriendshipChecker.IsUsersAreFriendsByConversationGuidANDid(Guid.Parse(convId), toUserId);
            }

            if (isFriends)
            {
                var messageHub = new MessagersHub(base.Context, base.Clients);
                NotifyHubModel hubResp = await messageHub.SendMessage(message, convId, toUserId);

                if (hubResp != null)
                {
                    string html = Notifications.GenerateHtml(NotificationType.NewMessage, hubResp.FromUser, hubResp.Culture);
                    Clients.Clients(hubResp.ConnectionIds).NotifyAbout(html, null);
                }
            }
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
                        MyProfileModel fromUser = UsWork.GetCurrentProfileInfo(cookies.Value);

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
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            cryptoHub.CreateNewCryptoConversation(create_request);
        }


        public async Task InviteUsersToCryptoChat(string offerToCriptoChat, Guid cryptoConversationGuidID)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            NotifyHubModel response = await cryptoHub.InviteUsersToCryptoChat(offerToCriptoChat, cryptoConversationGuidID);
            string html = Notifications.GenerateHtml(NotificationType.NewInviteSecretChat, response.FromUser, response.Culture);
            Clients.Clients(response.ConnectionIds).NotifyAbout(html, response.PublicDataToExcange);
        }

        public async Task AcceptInvite(string ansver_to_cripto_chat)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            NotifyHubModel response = await cryptoHub.AcceptInvite(ansver_to_cripto_chat);
            string html = Notifications.GenerateHtml(NotificationType.AcceptYourInviteSecretChat, response.FromUser, response.Culture );
            Clients.Clients(response.ConnectionIds).NotifyAbout(html, response.PublicDataToExcange);
        }

        public async Task SendMessage(string message)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            var response = await cryptoHub.SendMessage(message);
            if (response != null)
            {
                string html = Notifications.GenerateHtml(NotificationType.NewMessageSecret, response.FromUser, response.Culture);
                Clients.Clients(response.ConnectionIds).NotifyAbout( html, null );
            }
        }





        public async Task CreateAndInvite(int calleUserId)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
            NotifyHubModel responce = await videoHub.CreateAndInvite(calleUserId);
            string html = Notifications.GenerateHtml(NotificationType.NewInviteVideoConference, responce.FromUser, responce.Culture);
            Clients.Clients(responce.ConnectionIds).NotifyAbout(html, null);
        }

        public async Task Invite(string callOffer, Guid videoConverenceGuidID)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
            videoHub.Invite(callOffer, videoConverenceGuidID);
        }

        public async Task ConfirmInvite(Guid guid, string callAnswer)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
            videoHub.ConfirmInvite(guid, callAnswer);
        }

        public async Task ExchangeICandidates(dynamic iceCandidate, Guid guidID)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
            videoHub.ExchangeICandidates(iceCandidate, guidID);
        }

        public async Task CloseVideoConverence(Guid guidID)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
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
                Clients.Clients(connections.ConnectionId).NotifyAbout(html, null);
            }
            string htmlToCaller = UserFriendshipResponce.GenerateHtml(connections.CultureCode[0]);
            Clients.Caller.AddUserResponce(htmlToCaller);
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

            NotifyHubModel response = await userWorker.AcceptInviteToContacts(session, userID);
            string html = Notifications.GenerateHtml( NotificationType.AcceptFriendship, response.FromUser, response.Culture);
            Clients.Clients(response.ConnectionIds).NotifyAbout(html, null);
        }

    }
}