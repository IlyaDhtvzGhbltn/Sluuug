using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Slug.Context;
using Slug.Model;

namespace Slug.Hubs
{
    public class videoChatInviteHub : Hub
    {
        public void Invite(dynamic callOffer)
        {
            var cookies = base.Context.Request.Cookies;
            Cookie sessionId = cookies["session_id"];

            var UsWork = new UserWorker();
            var userInfo = UsWork.GetUserInfo(sessionId.Value);

            Clients.Others.GotInvite(userInfo.Name, userInfo.SurName, callOffer, userInfo.UserId); 
        }

        public void ConfirmInvite(dynamic callAnswer)
        {
            Clients.Others.ConfirmInvite(callAnswer);
        }

        public void ExchangeICandidates(dynamic iceCandidate)
        {
            Clients.Others.exchangeCandidates(iceCandidate);
        }
    }
}