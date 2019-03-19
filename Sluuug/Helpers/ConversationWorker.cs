using Context;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class ConversationWorker
    {
        public ConversationsModel GetPreConversations(int userId)
        {
            var convs = new ConversationsModel();
            
            using (var context = new DataBaseContext())
            {
                List<Conversation> conversations = 
                    context.Conversations
                    .Where(x => x.UserId == userId)
                    .OrderBy(x => x.Id)
                    .ToList();
                if (conversations.Count >= 1)
                {
                    var UsWork = new UserWorker();
                    convs.Cnv = new List<CutConversation>();
                    foreach (var conv in conversations)
                    {
                        IQueryable<Message> messages = context.Messangers
                            .Where(x => x.ConvarsationId == conv.ConversationId);

                        var message = messages.OrderBy(x => x.Id);
                        if (message.Count() >= 1)
                        {
                            var lastMessage = message.ToList().Last();

                            int lastMessageUserId = lastMessage.UserId;
                            var lastSayUser = UsWork.GetUserInfo(lastMessageUserId);
                            var c = new CutConversation();

                            c.AvatarPath = lastSayUser.AvatarUri;
                            c.InterlocutorName = lastSayUser.Name;
                            c.InterlocutorSurName = lastSayUser.SurName;
                            c.LastMessage = lastMessage.Text;
                            c.Id = conv.ConversationId;

                            convs.Cnv.Add(c);
                        }
                    }
                }
            }
            return convs;
        }
    }
}