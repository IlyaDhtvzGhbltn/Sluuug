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
        private int multiple = 10;

        public DialogModel GetMessanges(Guid convId, int page)
        {
            if (page <= 0)
                page = 1;

            var dModel = new DialogModel();
            var messangs = new List<DialogMessage>();

            using (var context = new DataBaseContext())
            {
                int multipleCount = context.Messangers
                                        .Where(x => x.ConvarsationGuidId == convId)
                                        .Count();

                decimal del = ( (decimal)multipleCount / (decimal)this.multiple);
                int resMultiple = Convert.ToInt32( Math.Ceiling(del) );
                if (page > resMultiple)
                    page = resMultiple;

                dModel.Page = resMultiple;


                var msgs = context.Messangers
                    .Where(x => x.ConvarsationGuidId == convId)
                    .OrderBy(x=>x.Id)
                    .Skip( (resMultiple - page) * multiple)
                    .Take( multiple)
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