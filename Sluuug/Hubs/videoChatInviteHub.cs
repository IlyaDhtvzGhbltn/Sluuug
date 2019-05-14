using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context;
using Slug.Context.Attributes;
using Slug.Context.Dto.Messages;
using Slug.Helpers;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Model;
using Slug.Helpers.BaseController;

namespace Slug.Hubs
{
    public class VideoChatInviteHub : Hub
    {
        private UsersConnectionHandler connectionsHandler { get; } = new UsersConnectionHandler();
        private VideoConferenceHandler videoConferenceHandler { get; } = new VideoConferenceHandler();
        private UsersHandler userInfoHandler { get; } = new UsersHandler();

        public VideoChatInviteHub(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        /// <summary>
        /// use like notify about new video-conference invite
        /// </summary>
        /// <param name="calleUserId"></param>
        /// <returns></returns>
        public async Task<PartialHubResponse> CreateAndInvite(int calleUserId)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            CutUserInfoModel userInfo = this.userInfoHandler.GetFullUserInfo(cookies.Value);

            Guid guid = this.videoConferenceHandler.Create(userInfo.UserId, calleUserId);
            IList<string> UserRecipientsConnectionIds = new List<string>();
            UserRecipientsConnectionIds = this.connectionsHandler.GetConnectionById(calleUserId);

            Clients.Clients(UserRecipientsConnectionIds).CalleInviteToRedirect(guid, userInfo.UserId);
            Clients.Caller.CallerGuidToRedirect(guid);

            var response = new PartialHubResponse();
            response.ConnectionIds = UserRecipientsConnectionIds;
            response.FromUser = userInfo;
            return response;
        }

        public void Invite(string callOffer, Guid videoConverenceGuidID)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            CutUserInfoModel userInfo = this.userInfoHandler.GetFullUserInfo(cookies.Value);

            this.videoConferenceHandler.UpdateConferenceOffer(callOffer, userInfo.UserId, videoConverenceGuidID);

            int invitedID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceGuidID, userInfo.UserId);
            IList<string> inviteConnectionsID = this.connectionsHandler.GetConnectionById(invitedID);

            Clients.Clients(inviteConnectionsID).GotInvite(videoConverenceGuidID, callOffer);
        }

        public async Task<PartialHubResponse> ConfirmInvite(Guid videoConverenceID, string callAnswer)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            CutUserInfoModel userInfo = this.userInfoHandler.GetFullUserInfo(cookies.Value);
            int callerNeedAnswerID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceID, userInfo.UserId);

            this.videoConferenceHandler.SaveAnswerVideoConference(callAnswer, videoConverenceID);
            IList<string> inviteConnectionsID = this.connectionsHandler.GetConnectionById(callerNeedAnswerID);

            Clients.Clients(inviteConnectionsID).ConfirmInvite(videoConverenceID, callAnswer);

            var response = new PartialHubResponse();
            response.ConnectionIds = inviteConnectionsID;
            response.FromUser = userInfo;
            return response;
        }

        public void ExchangeICandidates(dynamic iceCandidate, Guid videoConverenceID)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            CutUserInfoModel userInfo = this.userInfoHandler.GetFullUserInfo(cookies.Value);
            int otherUserID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceID, userInfo.UserId);

            IList<string> inviteConnectionsID = this.connectionsHandler.GetConnectionById(otherUserID);


            Clients.Clients(inviteConnectionsID).exchangeCandidates(iceCandidate);
        }

        public void CloseVideoConverence(Guid ID)
        {
            this.videoConferenceHandler.CloseConverence(ID);
            int[] usersParticipants = this.videoConferenceHandler.GetVideoConferenceParticipantsIDs(ID);
            IList<string> UserRecipientsConnectionIds = new List<string>();
            UserRecipientsConnectionIds = this.connectionsHandler.GetConnectionsByIds(usersParticipants);

            Clients.Clients(UserRecipientsConnectionIds).Close();
        }
    }
}