using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context;
using Slug.Context;
using Slug.Model;


namespace Slug.Helpers
{
    public class DialogWorker
    {
        private UserWorker UserWorker = new UserWorker();


        public DialogModel GetLast100msgs(int convId)
        {
            var messangs = new List<DialogMessage>();
            var dModel = new DialogModel();

            using (var context = new DataBaseContext())
            {
                var msgs = context.Messangers
                    .Where(x => x.ConvarsationId == convId)
                    .OrderBy(x=>x.Id)
                    .Take(100)
                    .ToList();

                msgs.ForEach(message => 
                {
                    var msg = new DialogMessage();
                    msg.Text = message.Text;
                    msg.SendTime = message.SendingDate;
                    msg.UserId = message.UserId;
                    msg.AvatarPath = UserWorker.GetUserInfo(message.UserId).AvatarUri;
                    msg.UserName = UserWorker.GetUserInfo(message.UserId).Name;
                    msg.Text = message.Text;
                    messangs.Add(msg);
                });
                dModel.Messages = messangs;
            }
            return dModel;
        }
    }
}