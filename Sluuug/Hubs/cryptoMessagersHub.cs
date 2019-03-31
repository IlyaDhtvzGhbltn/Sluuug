using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Slug.Context;

namespace Slug.Hubs
{
    public class cryptoMessagersHub : Hub
    {
        public void CreateNew(dynamic inviters, string p, string g)
        {
            Cookie Session = Context.Request.Cookies["session_id"];
            UserWorker worker = new UserWorker();
            var from = worker.GetUserInfo(Session.Value);

            Clients.Others.Invite(from, p, g);
        }

        public void AcceptInvite(string AB)
        {
            Clients.Others.accept(AB);
        }
    }
}