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
        public async Task SendMessage(string session_id, string message, int convId)
        {
            var UsWork = new UserWorker();
            var user = UsWork.GetUserInfo(session_id);
            if (user != null)
            {
                DialogWorker dW = new DialogWorker();
                var clearMsg = System.Net.WebUtility.HtmlDecode(message);
                await dW.SaveMsg(convId, user.UserId, clearMsg);

                Clients.All.sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), convId);
            }
        }
    }
}