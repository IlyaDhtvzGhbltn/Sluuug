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
            CryptoChatWorker CrWorker = new CryptoChatWorker();
            UserWorker UsWorker = new UserWorker();
            var cookies = base.Context.Request.Cookies["session_id"];
            var id = UsWorker.GetUserInfo(cookies.Value).UserId;
            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(ansver_to_cripto_chat);


            CrWorker.UpdateAcceptCryptoChat(id, cryptoConversation.ConvGuidId);
            Clients.Others.AcceptInvitation(ansver_to_cripto_chat);
        }

        public async Task SendMessage(string message)
        {
            CryptoChatWorker CrWorker = new CryptoChatWorker();
            UserWorker UsWorker = new UserWorker();
            Cookie cookies = base.Context.Request.Cookies["session_id"];
            int id = UsWorker.GetUserInfo(cookies.Value).UserId;

            string uri = base.Context.QueryString["URL"];
            var reg = new Regex("=.{36}");
            MatchCollection matches = reg.Matches(uri);
            string guidChatId = matches[0].ToString().Substring(1);
            
            await CrWorker.SaveSecretMessageHashAsync(guidChatId, id, message);
            var Info = UsWorker.GetUserInfo(id);

            Clients.All.NewMessage(message, Info.AvatarUri, Info.Name, DateTime.Now);
        }
    }
}