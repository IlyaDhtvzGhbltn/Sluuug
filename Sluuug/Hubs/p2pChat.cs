using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Sluuug.Hubs
{
    public class p2pChat : Hub
    {
        public Task SendMessage(string session_id, string message)
        {
            Clients.All.sendAsync("https://res.cloudinary.com/dlk1sqmj4/image/upload/v1552742058/usa1.jpg", "XXX", message);
            return null;
        }
    }
}