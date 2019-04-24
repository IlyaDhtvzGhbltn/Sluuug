using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context;
using Slug.Context.Attributes;
using Slug.Helpers;
using Slug.Model;

namespace Slug.Hubs
{
    public class VideoChatInviteHub : Hub
    {
        public VideoChatInviteHub(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        public void CreateAndInvite(int calleUserId)
        {
            var VCWorker = new VideoConferenceWorker(Context, calleUserId);
            var UsWork = new UserWorker();

            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var userInfo = UsWork.GetUserInfo(cookies.Value);

            Guid guid = VCWorker.Create(userInfo.UserId, calleUserId);
            var connectionWorker = new UserConnectionWorker();
            IList<string> UserRecipientsConnectionIds = new List<string>();
            UserRecipientsConnectionIds = connectionWorker.GetConnectionById(calleUserId);

            Clients.Clients(UserRecipientsConnectionIds).CalleInviteToRedirect(guid, userInfo.UserId);
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