using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Slug.Hubs
{
    public class CutMessagersHub : Hub
    {
        public void SendCut(string messageState, Guid dialog)
        {
            Clients.All.hello();
        }
    }
}