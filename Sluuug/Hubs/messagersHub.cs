using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context;
using Slug.Context.Dto.Messages;
using Slug.Helpers;
using Slug.Helpers.BaseController;
using Slug.Hubs;
using Slug.Model;
using Slug.Model.Users;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;


namespace Sluuug.Hubs
{
    public class MessagersHub : Hub
    {
        public MessagersHub(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        public async Task<NotifyHubModel> SendMessage(string message, string convId, int toUserId)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                int toUserID = toUserId;
                var UserRecipientsConnectionIds = new UserConnectionIdModel();
                Cookie cookies = Context.Request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
                var userHandler = new UsersHandler();
                var clearMsg = System.Net.WebUtility.HtmlDecode(message);

                BaseUser user = userHandler.GetCurrentProfileInfo(cookies.Value, false);
                if (user != null)
                {
                    var dialogWorker = new UsersDialogHandler();
                    Guid convGuidID = Guid.Empty;
                    if (convId == "0")
                    {
                        convGuidID = userHandler.GetConversationId(cookies.Value, toUserId);
                    }
                    else
                    {
                        convGuidID = Guid.Parse(convId);
                        toUserID = dialogWorker.GetConversatorsIds(convGuidID).Where(x => x != user.UserId).First();
                    }

                    await dialogWorker.SaveMsg(convGuidID, user.UserId, clearMsg);
                    var connectionWorker = new UsersConnectionHandler();
                    UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUserID);

                    var model = new DialogMessage()
                    {
                        AvatarPath = user.AvatarResizeUri,
                        UserName = user.Name,
                        UserSurname = user.SurName,
                        SendTime = DateTime.Now.ToString("yyyy-mm-dd"),
                        Text = clearMsg,
                    };
                    string html = Slug.Helpers.HTMLGenerated.DialogMessage.GenerateHtml(model);

                    Clients.Caller.sendAsync(html, convGuidID);
                    Clients.Clients(UserRecipientsConnectionIds.ConnectionId).sendAsync(html, convGuidID);
                }
                var responce = new NotifyHubModel();
                responce.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
                responce.FromUser = user;
                responce.Culture = UserRecipientsConnectionIds.CultureCode[0];
                return responce;
            }
            else return null;
        }
    }
}