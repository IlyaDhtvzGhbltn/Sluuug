using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context.Dto.Messages;
using Slug.Helpers;
using Sluuug.Hubs;

namespace Slug.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {

        public void OpenConnect()
        {
            var UCW = new UserConnectionWorker();
            string session = base.Context.Request.Cookies["session_id"].Value;
            string connection = Context.ConnectionId;
            UCW.AddConnection(connection, session);
        }

        public void CloseConnect()
        {
            var UCW = new UserConnectionWorker();
            string session = base.Context.Request.Cookies["session_id"].Value;
            string connection = Context.ConnectionId;
            UCW.CloseConnection(connection, session);
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
            var messageHub = new MessagersHub(base.Context, base.Clients);
            PartialHubResponse userMessageHubResp = await messageHub.SendMessage(message, convId, toUserId);

            Clients.Clients(userMessageHubResp.ConnectionIds).NotifyAbout(
                "MSG", 
                userMessageHubResp.FromUser.Name, 
                userMessageHubResp.FromUser.SurName, 
                userMessageHubResp.FromUser.AvatarUri, null);
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

            Clients.Clients(response.ConnectionIds).NotifyAbout(
                "C_MSG",
                response.FromUser.Name,
                response.FromUser.SurName,
                response.FromUser.AvatarUri
                );
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
    }
}