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
        public async Task<NotifyHubModel> CreateAndInvite(int calleUserId)
        {
            var info = CultureInfo.CurrentCulture;
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.GetCurrentProfileInfo(cookies.Value);
            Guid guid = this.videoConferenceHandler.Create(userInfo.UserId, calleUserId);
            var UserRecipientsConnectionIds = new UserConnectionIdModel();
            UserRecipientsConnectionIds = this.connectionsHandler.GetConnectionById(calleUserId);

            var model = new IncomingInviteModel()
            {
                CallerName = userInfo.Name,
                CallerSurName = userInfo.SurName,
                InviterID = userInfo.UserId,
                ConferenceID = guid,
                AvatarResizeUri = Resize.ResizedAvatarUri(userInfo.AvatarResizeUri, ModTypes.c_scale, 50, 50)
            };
            var culture = CultureInfo.CurrentCulture;
            string html = Helpers.HTMLGenerated.VideoConferenceInviteToRedirect.GenerateHtml(model, UserRecipientsConnectionIds.CultureCode[0]);
            model.Html = html;
            string json = JsonConvert.SerializeObject(model);

            Clients.Clients(UserRecipientsConnectionIds.ConnectionId).CalleInviteToRedirect(json);
            Clients.Caller.CallerGuidToRedirect(guid);

            var response = new NotifyHubModel();
            response.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
            response.FromUser = userInfo;
            response.Culture = UserRecipientsConnectionIds.CultureCode[0];
            return response;
        }

        public void Invite(string callOffer, Guid videoConverenceGuidID)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.GetCurrentProfileInfo(cookies.Value);

            this.videoConferenceHandler.UpdateConferenceOffer(callOffer, userInfo.UserId, videoConverenceGuidID);

            int invitedID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceGuidID, userInfo.UserId);
            var inviteConnectionsID = connectionsHandler.GetConnectionById(invitedID);

            Clients.Clients(inviteConnectionsID.ConnectionId).GotInvite(videoConverenceGuidID, callOffer);
        }

        public async Task<NotifyHubModel> ConfirmInvite(Guid videoConverenceID, string callAnswer)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.GetCurrentProfileInfo(cookies.Value);
            int callerNeedAnswerID = this.videoConferenceHandler.GetVideoConferenceParticipantID(videoConverenceID, userInfo.UserId);

            this.videoConferenceHandler.SaveAnswerVideoConference(callAnswer, videoConverenceID);
            var inviteConnectionsID = connectionsHandler.GetConnectionById(callerNeedAnswerID);

            Clients.Clients(inviteConnectionsID.ConnectionId).ConfirmInvite(videoConverenceID, callAnswer);

            var response = new NotifyHubModel();
            response.ConnectionIds = inviteConnectionsID.ConnectionId;
            response.FromUser = userInfo;
            return response;
        }

        public void ExchangeICandidates(dynamic iceCandidate, Guid videoConverenceID)
        {
            Cookie cookies = base.Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            BaseUser userInfo = this.userInfoHandler.GetCurrentProfileInfo(cookies.Value);
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