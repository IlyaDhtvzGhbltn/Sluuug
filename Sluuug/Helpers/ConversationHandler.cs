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
    public class ConversationHandler
    {
        public ConversationsModel GetPreConversations(int userId)
        {
            var convs = new ConversationsModel();
            convs.Cnv = new List<CutConversation>();

            using (var context = new DataBaseContext())
            {
                var ConversationGroup = context.ConversationGroup
                    .Where(x => x.UserId == userId).ToList();
                var UsWork = new UsersHandler();

                foreach (var dialog in ConversationGroup)
                {
                    List<Conversation> conversations =
                        context.Conversations
                        .Where(x => x.ConversationGuidId == dialog.ConversationGuidId)
                        .OrderBy(x => x.Id)
                        .ToList();
                    if (conversations.Count >= 1)
                    {
                        foreach (var conv in conversations)
                        {
                            IQueryable<Message> messages = context.Messangers
                                .Where(x => x.ConvarsationGuidId == conv.ConversationGuidId);

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
                                c.GuidId = conv.ConversationGuidId;

                                convs.Cnv.Add(c);
                            }
                        }
                    }
                }
            }
            return convs;
        }
    }
}