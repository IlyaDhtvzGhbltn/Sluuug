using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Slug.Context;
using Slug.Helpers;
using Slug.Hubs;

namespace Sluuug.Hubs
{
    public class MessagersHub : Hub
    {
        public MessagersHub(HubCallerContext context, IHubCallerConnectionContext<dynamic> clients)
        {
            this.Context = context;
            this.Clients = clients;
        }

        public async Task SendMessage(string message, string convId, int toUserId)
        {
            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var UsWork = new UserWorker();
            var clearMsg = System.Net.WebUtility.HtmlDecode(message);

            var user = UsWork.GetUserInfo(cookies.Value);
            if (user != null)
            {
                DialogWorker dW = new DialogWorker();
                Guid conversation = Guid.Empty;
                int toUserID = toUserId;
                if (convId == "0")
                {
                    conversation = UsWork.GetConversationId(cookies.Value, toUserId);
                }
                else
                {
                    conversation = Guid.Parse(convId);
                    toUserID = 2;
                }

                await dW.SaveMsg(conversation, user.UserId, clearMsg);
                Clients.All.sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), conversation);
            }
        }
    }
}