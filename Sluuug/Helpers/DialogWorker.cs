using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Context;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Model;


namespace Slug.Helpers
{
    public class DialogWorker
    {
        private UserWorker UserWorker = new UserWorker();

        public DialogModel GetLast100msgs(Guid convId)
        {
            var messangs = new List<DialogMessage>();
            var dModel = new DialogModel();

            using (var context = new DataBaseContext())
            {
                var msgs = context.Messangers
                    .Where(x => x.ConvarsationGuidId == convId)
                    .OrderBy(x=>x.Id)
                    .Take(100)
                    .ToList();

                msgs.ForEach(message => 
                {
                    var msg = new DialogMessage();
                    msg.Text = message.Text;
                    msg.SendTime = message.SendingDate.ToString("yyyy-mm-dd");
                    msg.UserId = message.ConvarsationGuidId;
                    msg.AvatarPath = UserWorker.GetUserInfo(message.UserId).AvatarUri;
                    msg.UserName = UserWorker.GetUserInfo(message.UserId).Name;
                    msg.Text = message.Text;
                    messangs.Add(msg);
                });
                dModel.Messages = messangs;
            }
            return dModel;
        }

        public int[] GetConversatorsIds(Guid convId)
        {
            using (var context = new DataBaseContext())
            {
                var conversationEntities = context.ConversationGroup.Where(x => x.ConversationGuidId == convId).ToList();
                if (conversationEntities.Count > 0)
                {
                    int[] ids = new int[conversationEntities.Count()];
                    for (int i = 0; i < conversationEntities.Count; i++)
                    {
                        ids[i] = conversationEntities[i].UserId;
                    }
                    return ids;
                }
            }
            return null;
        }

        public async Task SaveMsg(Guid convId, int userId, string text)
        {
            using (var context = new DataBaseContext())
            {
                var msg = new Message();
                msg.ConvarsationGuidId = convId;
                msg.Text = text;
                msg.UserId = userId;
                msg.SendingDate = DateTime.Now;
                context.Messangers.Add(msg);
                await context.SaveChangesAsync();
            }
        }


    }
}