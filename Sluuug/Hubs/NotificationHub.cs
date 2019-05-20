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
            UCW.AddConnection(connection, session);
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
            int myID = userHandler.GetFullUserInfo(session).UserId;
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
                PartialHubResponse userMessageHubResp = await messageHub.SendMessage(message, convId, toUserId);

                Clients.Clients(userMessageHubResp.ConnectionIds).NotifyAbout(
                    "MSG",
                    userMessageHubResp.FromUser.Name,
                    userMessageHubResp.FromUser.SurName,
                    userMessageHubResp.FromUser.AvatarUri, null);
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
                        FullUserInfoModel fromUser = UsWork.GetFullUserInfo(cookies.Value);

                        if (participants.Contains(fromUser.UserId))
                        {
                            int toUserID = participants.Where(x => x != fromUser.UserId).First();
                            bool isFriends = FriendshipChecker.CheckUsersFriendshipByIDS(fromUser.UserId, toUserID);
                            if (isFriends)
                            {

                                bool fromUserQuickSett = context.Users.Where(x => x.Id == fromUser.UserId).First().Settings.QuickMessage;
                                bool toUserQuickSett = context.Users.Where(x => x.Id == toUserID).First().Settings.QuickMessage;

                                if (fromUserQuickSett && toUserQuickSett)
                                {
                                    var connectionHandler = new UsersConnectionHandler();
                                    IList<string> UserRecipientsConnectionIds = connectionHandler.GetConnectionById(toUserID);
                                    Clients.Clients(UserRecipientsConnectionIds).getCutMessage(message);
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
            PartialHubResponse response = await cryptoHub.InviteUsersToCryptoChat(offerToCriptoChat, cryptoConversationGuidID);
            Clients.Clients(response.ConnectionIds).NotifyAbout(
                "ICC",
                response.FromUser.Name,
                response.FromUser.SurName,
                response.FromUser.AvatarUri, 
                response.PublicDataToExcange
                );
        }

        public async Task AcceptInvite(string ansver_to_cripto_chat)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            PartialHubResponse response = await cryptoHub.AcceptInvite(ansver_to_cripto_chat);
            Clients.Clients(response.ConnectionIds).NotifyAbout(
                "ICC_A",
                response.FromUser.Name,
                response.FromUser.SurName,
                response.FromUser.AvatarUri
                );
        }

        public async Task SendMessage(string message)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            var response = await cryptoHub.SendMessage(message);
            if (response != null)
            {
                Clients.Clients(response.ConnectionIds).NotifyAbout(
                    "C_MSG",
                    response.FromUser.Name,
                    response.FromUser.SurName,
                    response.FromUser.AvatarUri
                    );
            }
        }





        public async Task CreateAndInvite(int calleUserId)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
            PartialHubResponse responce = await videoHub.CreateAndInvite(calleUserId);
            Clients.Clients(responce.ConnectionIds).NotifyAbout(
                "VC",
                responce.FromUser.Name,
                responce.FromUser.SurName,
                responce.FromUser.AvatarUri, null);
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
            CutUserInfoModel userSenderFriendsRequest = userWorker.AddInviteToContacts(session, userID);
            if (userSenderFriendsRequest != null)
            {
               IList<string> connections = connectionsWorker.GetConnectionById(userID);
                Clients.Clients(connections).NotifyAbout(
                    "FRND",
                    userSenderFriendsRequest.Name,
                    userSenderFriendsRequest.SurName,
                    userSenderFriendsRequest.AvatarUri);
            }
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

            PartialHubResponse response = await userWorker.AcceptInviteToContacts(session, userID);

            Clients.Clients(response.ConnectionIds).NotifyAbout(
                "ACC_FRND",
                response.FromUser.Name,
                response.FromUser.SurName,
                response.FromUser.AvatarUri);
        }

    }
}