﻿using Context;
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
        public ConversationGroupModel GetPreConversations(int userId)
        {
            var convs = new ConversationGroupModel();
            convs.Cnv = new List<ConversationModel>();

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
                                BaseUser friendInterlocutor = UsWork.BaseUser(InterlocutorID);

                                var lastMessage = message.ToList().Last();
                                int lastMessageUserId = lastMessage.UserId;
                                BaseUser lastSayUser = UsWork.BaseUser(lastMessageUserId);
                                var c = new ConversationModel();

                                c.InterlocutorAvatar = Resize.ResizedAvatarUri(friendInterlocutor.AvatarResizeUri, ModTypes.c_scale, 100, 100);
                                c.InterlocutorName = friendInterlocutor.Name;
                                c.InterlocutorSurName = friendInterlocutor.SurName;

                                c.LastMessageSenderName = lastSayUser.Name;
                                c.LastMessageSenderSurName = lastSayUser.SurName;
                                if (lastMessage.Text.Length > 50)
                                    c.LastMessage = lastMessage.Text.Substring(0, 49) + "...";
                                else
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