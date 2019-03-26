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
        public void Invite(SessionDescription callOffer)
        {
            var Contx = base.Context;
            var UsWork = new UserWorker();
            var userInfo = UsWork.GetUserInfo(callOffer.session);
            var inviteOffer = new SessionDescription
            {
                type = callOffer.type,
                sdp = callOffer.sdp
            };
            Clients.Others.SendInvite(userInfo.Name, userInfo.SurName, inviteOffer, userInfo.UserId); 
        }

        public void ConfirmInvite(SessionDescription callAnswer)
        {
            var inviteAnswer = new SessionDescription
            {
                type = callAnswer.type,
                sdp = callAnswer.sdp
            };
            Clients.Others.ConfirmInvite(inviteAnswer);
        }
    }
}