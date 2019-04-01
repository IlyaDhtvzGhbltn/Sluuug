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

namespace Slug.Hubs
{
    public class cryptoMessagersHub : Hub
    {
        public void CreateRequest(string create_request)
        {
            var Cookie = base.Context.Request.Cookies;
            var session_id = Cookie["session_id"];
            var uW = new UserWorker();
            var UserInfo = uW.GetUserInfo(session_id.Value);

            var CryptoChatRequest = JsonConvert.DeserializeObject<CryptoChatRequest>(create_request);
            
            var CryptWorker = new CryptoChatWorker();
            var charGuid = CryptWorker.CreateNewCryptoChat((CryptoChatType)CryptoChatRequest.Type, CryptoChatRequest.Inviters, UserInfo.UserId);
            Clients.Caller.NewCryptoChatResponce(charGuid);
        }

        public void InviteToCreatedNew(dynamic offer_to_cripto_chat)
        {
            Cookie Session = Context.Request.Cookies["session_id"];
            UserWorker worker = new UserWorker();
            var from = worker.GetUserInfo(Session.Value);

            Clients.Others.Invite(offer_to_cripto_chat);
        }

        public void AcceptInvite(dynamic ansver_to_cripto_chat)
        {
            Clients.All.accept(ansver_to_cripto_chat);
        }
    }

    public class CryptoChatRequest
    {
        public int Type { get; set; }

        public List<string> Inviters { get; set; }
    }
}