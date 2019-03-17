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
    public class p2pChat : Hub
    {
        public async Task SendMessage(string session_id, string message, int convId)
        {
            var UsWork = new UserWorker();
            var user = UsWork.GetUserInfo(session_id);
            if (user != null)
            {
                DialogWorker dW = new DialogWorker();
                await dW.SaveMsg(convId, user.UserId, message);

                Clients.All.sendAsync(user.AvatarUri, user.Name, message);
            }
        }
    }
}