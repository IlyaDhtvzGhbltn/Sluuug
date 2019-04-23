using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Slug.Helpers;

namespace Slug.Hubs
{
    public class NotificationHub : Hub
    {

        public void OpenConnect()
        {
            var UCW = new UserConnectionWorker();
            string session = base.Context.Request.Cookies["session_id"].Value;
            string connection = Context.ConnectionId;
            UCW.AddConnection(connection, session);
        }

        public void CloseConnect()
        {
            var UCW = new UserConnectionWorker();
            string session = base.Context.Request.Cookies["session_id"].Value;
            string connection = Context.ConnectionId;
            UCW.CloseConnection(connection, session);
        }

        public async Task NewChatMessage()
        {
            //Clients.Clients().GotNewChatMessage();
        }

        public async Task NewCryptoChatMessage()
        {
            //Clients.Others.GotNewCryptoChatMessage();
        }

        public async Task NewVideoConverence()
        {
            //Clients.Others.NewVideoConverence();
        }
    }
}