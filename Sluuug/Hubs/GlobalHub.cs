using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Slug.Hubs
{
    public class GlobalHub : Hub
    {
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