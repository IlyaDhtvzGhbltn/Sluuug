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
        public async Task SendMessage(string message, int convId)
        {
            Cookie cookies = base.Context.Request.Cookies["session_id"];
            var UsWork = new UserWorker();
            if (convId != -1)
            {

                var user = UsWork.GetUserInfo(cookies.Value);
                if (user != null)
                {
                    DialogWorker dW = new DialogWorker();
                    var clearMsg = System.Net.WebUtility.HtmlDecode(message);


                    await dW.SaveMsg(convId, user.UserId, clearMsg);
                    Clients.All.sendAsync(user.AvatarUri, user.Name, user.SurName, clearMsg, DateTime.Now.ToString("yyyy-mm-dd"), convId);
                }
            }
            else if (convId == -1)
            {
                sendFromUserPage();
            }
        }


        private void sendFromDialog()
        {

        }

        private void sendFromUserPage()
        {

        }
    }
}