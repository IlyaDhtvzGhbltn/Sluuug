using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context;
using Slug.Context.Dto.Messages;
using Slug.Helpers;
using Slug.Hubs;
using Slug.Model;

namespace Sluuug.Hubs
{
    public class MessagersHub : Hub
    {
        public MessagersHub(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        public async Task<PartialHubResponse> SendMessage(string message, string convId, int toUserId)
        {
            int toUserID = toUserId;
            IList<string> UserRecipientsConnectionIds = new List<string>();
            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var UsWork = new UserWorker();
            var clearMsg = System.Net.WebUtility.HtmlDecode(message);

            CutUserInfoModel user = UsWork.GetUserInfo(cookies.Value);
            if (user != null)
            {
                var dialogWorker = new DialogWorker();
                Guid convGuidID = Guid.Empty;
                if (convId == "0")
                {
                    convGuidID = UsWork.GetConversationId(cookies.Value, toUserId);
                }
                else
                {
                    convGuidID = Guid.Parse(convId);
                    toUserID = dialogWorker.GetConversatorsIds(convGuidID).Where(x => x != user.UserId).First();
                }

                await dialogWorker.SaveMsg(convGuidID, user.UserId, clearMsg);
                var connectionWorker = new UserConnectionWorker();
                UserRecipientsConnectionIds = connectionWorker.GetConnectionById(toUserID);

                Clients.Caller.sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), convGuidID);
                Clients.Clients(UserRecipientsConnectionIds).sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), convGuidID);
            }
            var responce = new PartialHubResponse();
            responce.ConnectionIds = UserRecipientsConnectionIds;
            responce.FromUser = user;

            return responce;
        }
    }
}