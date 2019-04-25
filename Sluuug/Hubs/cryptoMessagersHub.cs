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

        public async Task<PartialHubResponse> InviteUsersToCryptoChat(string offer_to_cripto_chat, Guid userInvited)
        {
            Cookie Session = Context.Request.Cookies["session_id"];
            UserWorker worker = new UserWorker();
            CutUserInfoModel fromUser = worker.GetUserInfo(Session.Value);

            PublicDataCryptoConversation cryptoConversation = JsonConvert.DeserializeObject<PublicDataCryptoConversation>(offer_to_cripto_chat);
            cryptoConversation.CreatorAvatar = fromUser.AvatarUri;
            cryptoConversation.CreatorName = fromUser.Name;

            var connectionWorker = new UserConnectionWorker();
            var cryptoChatWorker = new CryptoChatWorker();
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
            var connectionWorker = new UserConnectionWorker();
            var CrWorker = new CryptoChatWorker();
            var UsWorker = new UserWorker();

            var cookies = base.Context.Request.Cookies["session_id"];
            CutUserInfoModel userAccepter = UsWorker.GetUserInfo(cookies.Value);
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
            var connectionWorker = new UserConnectionWorker();
            var cryptoChatWorker = new CryptoChatWorker();

            UserWorker UsWorker = new UserWorker();
            Cookie cookies = base.Context.Request.Cookies["session_id"];
            int id = UsWorker.GetUserInfo(cookies.Value).UserId;

            string uri = base.Context.QueryString["URL"];
            var reg = new Regex("=.{36}");
            MatchCollection matches = reg.Matches(uri);
            string guidChatId = matches[0].ToString().Substring(1);
            
            await cryptoChatWorker.SaveSecretMessageHashAsync(guidChatId, id, message);
            CutUserInfoModel fromUser = UsWorker.GetUserInfo(id);

            IList<string> UserRecipientsConnectionIds = new List<string>();
            int toUser = cryptoChatWorker.GetInterlocutorID(Guid.Parse(guidChatId), fromUser.UserId);
            UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUser);

            Clients.Clients(UserRecipientsConnectionIds).NewMessage(message, fromUser.AvatarUri, fromUser.Name, DateTime.Now, guidChatId);
            Clients.Caller.NewMessage(message, fromUser.AvatarUri, fromUser.Name, DateTime.Now, guidChatId);

            var response = new PartialHubResponse();
            response.ConnectionIds = UserRecipientsConnectionIds;
            response.FromUser = fromUser;
            return response;
        }
    }
}