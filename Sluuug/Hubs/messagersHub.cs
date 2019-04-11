using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Slug.Context;
using Slug.Helpers;

namespace Sluuug.Hubs
{
    public class messagersHub : Hub
    {
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
                if (convId == "0")
                {
                    conversation = UsWork.GetConversationId(cookies.Value, toUserId);
                }
                else
                {
                    conversation = Guid.Parse(convId);
                }

                await dW.SaveMsg(conversation, user.UserId, clearMsg);
                Clients.All.sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), convId);
            }
        }
    }
}