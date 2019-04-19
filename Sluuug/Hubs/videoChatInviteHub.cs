using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Slug.Context;
using Slug.Context.Attributes;
using Slug.Helpers;
using Slug.Model;

namespace Slug.Hubs
{
    public class videoChatInviteHub : Hub
    {
        public void CreateAndInvite(int calleUserId)
        {
            var VCWorker = new VideoConferenceWorker(Context, calleUserId);
            var UsWork = new UserWorker();

            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var userInfo = UsWork.GetUserInfo(cookies.Value);

            Guid guid = VCWorker.Create(userInfo.UserId, calleUserId);
            Clients.Others.CalleInviteToRedirect(guid, userInfo.UserId);
            Clients.Caller.CallerGuidToRedirect(guid);
        }

        public void Invite(string callOffer, Guid videoConverenceGuidID)
        {
            var VCWorker = new VideoConferenceWorker();
            var UsWork = new UserWorker();

            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var userInfo = UsWork.GetUserInfo(cookies.Value);

            VCWorker.UpdateConferenceOffer(callOffer, userInfo.UserId, videoConverenceGuidID);

            Clients.Others.GotInvite(videoConverenceGuidID, callOffer); 
        }

        public void ConfirmInvite(Guid guid, string callAnswer)
        {
            var VCWorker = new VideoConferenceWorker();
            VCWorker.SaveAnswerVideoConference(callAnswer, guid);

            Clients.Others.ConfirmInvite(guid, callAnswer);
        }

        public void ExchangeICandidates(dynamic iceCandidate, Guid guidID)
        {
            Clients.Others.exchangeCandidates(iceCandidate);
        }

        public void CloseVideoConverence(Guid guidID)
        {
            var VCWorker = new VideoConferenceWorker();
            VCWorker.CloseConverence(guidID);
            Clients.All.Close();
        }
    }
}