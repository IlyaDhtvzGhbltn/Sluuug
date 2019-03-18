using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Sluuug.Hubs
{
    public class ContosoChatHub : Hub
    {
        public void NewContosoChatMessage(string name, string message)
        {
            Clients.All.addContosoChatMessageToPage(name, message);
        }
    }
}