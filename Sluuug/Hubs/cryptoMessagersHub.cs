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

namespace Slug.Hubs
{
    public class cryptoMessagersHub : Hub
    {
        public void CreateNewCryptoConversation(string create_request)
        {
            var Cookie = base.Context.Request.Cookies;
            var session_id = Cookie["session_id"];
            var uW = new UserWorker();
            var UserInfo = uW.GetUserInfo(session_id.Value);

            var CryptoChatResponce = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(create_request);
            CryptoChatResponce.CreatorName = UserInfo.Name;
            CryptoChatResponce.CreatorAvatar = UserInfo.AvatarUri;
            CryptoChatResponce.CreationDate = DateTime.Now;

            var CryptWorker = new CryptoChatWorker();
            var response = CryptWorker.CreateNewCryptoChat(CryptoChatResponce.Type, CryptoChatResponce.Participants, UserInfo.UserId);
            CryptoChatResponce.ConvGuidId = response.CryptoGuidId;
            CryptoChatResponce.CreatorUserId = UserInfo.UserId;
            CryptoChatResponce.Participants.Add(
                new Participant() {
                UserId = CryptoChatResponce.CreatorUserId
            });
            CryptoChatResponce.ExpireDate = response.ExpireDate;

            Clients.Caller.NewCryptoConversationCreated(CryptoChatResponce);
        }

        public void InviteUsersToCryptoChat(string offer_to_cripto_chat)
        {
            Cookie Session = Context.Request.Cookies["session_id"];
            UserWorker worker = new UserWorker();
            CutUserInfoModel from = worker.GetUserInfo(Session.Value);

            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(offer_to_cripto_chat);
            cryptoConversation.CreatorAvatar = from.AvatarUri;
            cryptoConversation.CreatorName = from.Name;

            Clients.Others.ObtainNewInvitation(cryptoConversation);
        }

        public void AcceptInvite(string ansver_to_cripto_chat)
        {
            Clients.Others.AcceptInvitation(ansver_to_cripto_chat);
        }
    }

}