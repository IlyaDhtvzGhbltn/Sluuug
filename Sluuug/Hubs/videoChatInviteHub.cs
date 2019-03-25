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
        public void Invite(Offer callOffer)
        {
            var UsWork = new UserWorker();
            var userInfo = UsWork.GetUserInfo(callOffer.session);
            var inviteOffer = new Offer
            {
                type = callOffer.type,
                sdp = callOffer.sdp
            };
            Clients.Others.SendInvite(userInfo.Name, userInfo.SurName, inviteOffer, userInfo.UserId
            ); 
        }
    }
}