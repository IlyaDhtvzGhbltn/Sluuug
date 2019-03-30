using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Slug.Hubs
{
    public class cryptoMessagersHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}