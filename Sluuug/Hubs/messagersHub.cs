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
            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var UsWork = new UserWorker();
            var clearMsg = System.Net.WebUtility.HtmlDecode(message);

            CutUserInfoModel user = UsWork.GetUserInfo(cookies.Value);
            if (user != null)
            {
                DialogWorker dW = new DialogWorker();
                Guid convGuidID = Guid.Empty;
                if (convId == "0")
                {
                    convGuidID = UsWork.GetConversationId(cookies.Value, toUserId);
                }
                else
                {
                    convGuidID = Guid.Parse(convId);
                    toUserID = dW.GetConversatorsIds(convGuidID).Where(x => x != user.UserId).First();
                }

                await dW.SaveMsg(convGuidID, user.UserId, clearMsg);
                Clients.All.sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), convGuidID);
            }
            var responce = new PartialHubResponse();
            responce.ToUserID = toUserID;
            responce.FromUserName = user.Name;
            responce.FromUserSurname = user.SurName;
            responce.FromUserAvatarUri = user.AvatarUri;

            return responce;
        }
    }
}