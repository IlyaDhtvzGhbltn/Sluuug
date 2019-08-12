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
using Slug.Helpers.Handlers.HandlersInterface;
using Slug.Hubs;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.Messager.SimpleChat;
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

                BaseUser user = userHandler.BaseUser(cookies.Value);
                bool isFriends = false;
                if (user != null)
                {
                    var dialogWorker = new UsersDialogHandler();
                    Guid convGuidID = Guid.Empty;
                    if (convId == "0")
                    {
                        convGuidID = userHandler.GetConversationId(cookies.Value, toUserId);
                        isFriends = FriendshipChecker.CheckUsersFriendshipByIDs(toUserId, user.UserId);
                    }
                    else
                    {
                        convGuidID = Guid.Parse(convId);
                        toUserID = dialogWorker.GetConversatorsIds(convGuidID).Where(x => x != user.UserId).First();
                        isFriends = FriendshipChecker.IsUsersAreFriendsByConversationGuidANDid(Guid.Parse(convId), user.UserId);
                    }

                    if (isFriends)
                    {
                        var dialogDisableHandler = new DisableConversationHandler();

                        await dialogWorker.SaveMsg(convGuidID, user.UserId, clearMsg);
                        await dialogDisableHandler.EnableDialog(convGuidID);

                        var connectionWorker = new UsersConnectionHandler();
                        UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUserID);

                        var messageModel = new MessageModel()
                        {
                            AvatarPath = Resize.ResizedAvatarUri(user.AvatarResizeUri, ModTypes.c_scale, 60, 60),
                            UserName = user.Name,
                            UserSurname = user.SurName,
                            Text = clearMsg,
                            SenderId = user.UserId,
                            ConversationId  = convGuidID
                        };
                        if (UserRecipientsConnectionIds != null)
                        {
                            Clients.Clients(UserRecipientsConnectionIds.ConnectionId)
                                .GetMessage(messageModel, Resize.ResizedAvatarUri(user.AvatarResizeUri, ModTypes.c_scale, 100, 100));
                            var responce = new NotifyHubModel();
                            responce.ConnectionIds = UserRecipientsConnectionIds.ConnectionId;
                            responce.FromUser = user;
                            responce.Culture = UserRecipientsConnectionIds.CultureCode.Count > 0 ? UserRecipientsConnectionIds.CultureCode[0] : null;
                            return responce;
                        }
                    }
                }
            }
            return null;
        }
    }
}