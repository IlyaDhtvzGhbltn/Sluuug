using Context;
using Slug.Context;
using Slug.Context.Tables;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.SimpleChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Slug.Model.Users;

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

                foreach (ConversationGroup dialog in ConversationGroup)
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
                                Guid dialogGUID = dialog.ConversationGuidId;
                                int InterlocutorID = context.ConversationGroup
                                    .Where(x => x.ConversationGuidId == dialogGUID &&
                                    x.UserId != userId).First().UserId;
                                BaseUser friendInterlocutor = UsWork.GetUserInfo(InterlocutorID);

                                var lastMessage = message.ToList().Last();
                                int lastMessageUserId = lastMessage.UserId;
                                BaseUser lastSayUser = UsWork.GetUserInfo(lastMessageUserId);
                                var c = new CutConversation();

                                c.InterlocutorAvatar = Resize.ResizedAvatarUri(friendInterlocutor.AvatarResizeUri, ModTypes.c_scale, 100, 100);
                                c.InterlocutorName = friendInterlocutor.Name;
                                c.InterlocutorSurName = friendInterlocutor.SurName;

                                c.LastMessageSenderAvatar = Resize.ResizedAvatarUri(lastSayUser.AvatarResizeUri, ModTypes.c_scale, 100, 100);
                                c.LastMessageSenderName = lastSayUser.Name;
                                c.LastMessageSenderSurName = lastSayUser.SurName;
                                c.LastMessage = lastMessage.Text.Substring(0, 120) + "...";
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