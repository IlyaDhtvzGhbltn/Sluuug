﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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




        public async Task SendMessage(string message, string convId, int toUserId)
        {
            var messageHub = new MessagersHub(base.Context, base.Clients);
            await messageHub.SendMessage(message, convId, toUserId);
        }




        public async Task CreateNewCryptoConversation(string create_request)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            cryptoHub.CreateNewCryptoConversation(create_request);
        }

        public async Task InviteUsersToCryptoChat(string offer_to_cripto_chat)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            cryptoHub.InviteUsersToCryptoChat(offer_to_cripto_chat);
        }
        public async Task AcceptInvite(string ansver_to_cripto_chat)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            cryptoHub.AcceptInvite(ansver_to_cripto_chat);
        }

        public async Task SendMessage(string message)
        {
            var cryptoHub = new CryptoMessagersHub(base.Context, base.Clients);
            await cryptoHub.SendMessage(message);
        }





        public async Task CreateAndInvite(int calleUserId)
        {
            var videoHub = new VideoChatInviteHub(base.Context, base.Clients);
            videoHub.CreateAndInvite(calleUserId);
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