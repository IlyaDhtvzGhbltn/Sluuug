using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Slug.Context;

namespace Sluuug.Hubs
{
    public class p2pChat : Hub
    {
        public Task SendMessage(string session_id, string message)
        {
            var UsWork = new UserWorker();
            var user = UsWork.GetUserInfo(session_id);
            if (user != null)
            {
                Clients.All.sendAsync(user.AvatarUri, user.Name, message);
            }
            return null;
        }
    }
}