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
using Slug.Model.VideoConference;
using Newtonsoft.Json;
using Slug.ImageEdit;
using System.Globalization;
using Slug.Model.Users;

namespace Slug.Hubs
{
    public class VideoConference : Hub
    {
        private UsersConnectionHandler connectionsHandler { get; } = new UsersConnectionHandler();
        private VideoConferenceHandler videoConferenceHandler { get; } = new VideoConferenceHandler();
        private UsersHandler userInfoHandler { get; } = new UsersHandler();

        public VideoConference(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        public async Task<NotificationModel> CreateAndInvite(int calleUserId)
        {
            var info = CultureInfo.CurrentCulture;
            Cookie cookies = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = userInfoHandler.ProfileInfo(cookies.Value);
            string guid = videoConferenceHandler.Create(userInfo.UserId, calleUserId);
            if (!string.IsNullOrWhiteSpace(guid))
            {
                Guid guidPars = Guid.Parse(guid);
                var UserRecipientsConnectionIds = new UserConnectionIdModel();
                UserRecipientsConnectionIds = this.connectionsHandler.GetConnectionById(calleUserId);
                var model = new IncomingInviteModel()
                {
                    CallerName = userInfo.Name,
                    CallerSurName = userInfo.SurName,
                    InviterID = userInfo.UserId,
                    ConferenceID = guidPars,
                    AvatarResizeUri = userInfo.SmallAvatar
                };
                Clients.Caller.CallerGuidToRedirect(guidPars);
                var response = new NotificationModel();
                response.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
                response.FromUser = userInfo;
                response.Culture = UserRecipientsConnectionIds.CultureCode.Count > 0 ? UserRecipientsConnectionIds.CultureCode[0] : null;
                return response;
            }
            return null;
        }

        public void Invite(string callOffer, Guid videoConverenceGuidID)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.ProfileInfo(cookies.Value);

            this.videoConferenceHandler.UpdateConferenceOffer(callOffer, userInfo.UserId, videoConverenceGuidID);

            int invitedID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceGuidID, userInfo.UserId);
            var inviteConnectionsID = connectionsHandler.GetConnectionById(invitedID);

            Clients.Clients(inviteConnectionsID.ConnectionId).GotInvite(videoConverenceGuidID, callOffer);
        }

        public async Task<NotificationModel> ConfirmInvite(Guid videoConverenceID, string callAnswer)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.ProfileInfo(cookies.Value);
            int callerNeedAnswerID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceID, userInfo.UserId);

            this.videoConferenceHandler.SaveAnswerVideoConference(callAnswer, videoConverenceID);
            var inviteConnectionsID = connectionsHandler.GetConnectionById(callerNeedAnswerID);

            Clients.Clients(inviteConnectionsID.ConnectionId).ConfirmInvite(videoConverenceID, callAnswer);

            var response = new NotificationModel();
            response.ConnectionIds = inviteConnectionsID.ConnectionId;
            response.FromUser = userInfo;
            return response;
        }

        public void ExchangeICandidates(dynamic iceCandidate, Guid videoConverenceID)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.ProfileInfo(cookies.Value);
            int otherUserID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceID, userInfo.UserId);

            var inviteConnectionsID = connectionsHandler.GetConnectionById(otherUserID);


            Clients.Clients(inviteConnectionsID.ConnectionId).exchangeCandidates(iceCandidate);
        }

        public void CloseVideoConverence(Guid ID)
        {
            this.videoConferenceHandler.CloseConverence(ID);
            int[] usersParticipants = this.videoConferenceHandler.GetVideoConferenceParticipantsIDs(ID);
            var UserRecipientsConnectionIds = connectionsHandler.GetConnectionsByIds(usersParticipants);
            Clients.Clients(UserRecipientsConnectionIds.ConnectionId).Close();
        }
    }
}