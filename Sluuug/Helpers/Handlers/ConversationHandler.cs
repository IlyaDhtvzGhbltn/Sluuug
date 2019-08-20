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
using Slug.Helpers.Handlers.HandlersInterface;
using System.Threading.Tasks;
using Slug.Context.Dto.News;

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
                    .Where(x => x.UserId == userId
                    ).ToList();
                var UsWork = new UsersHandler();

                foreach (ConversationGroup dialog in ConversationGroup)
                {
                    var conversations = context.Conversations
                        .Where(x => x.ConversationGuidId == dialog.ConversationGuidId &&
                            context.DisableDialogs.FirstOrDefault(d=>
                            d.ConversationId == dialog.ConversationGuidId &&
                            d.UserDisablerId == userId) == null)
                        .ToList();

                    if (conversations.Count >= 1)
                    {
                        foreach (Conversation conv in conversations)
                        {

                            Message lastMessage = context.Messangers
                                .Where(x => x.ConvarsationGuidId == conv.ConversationGuidId)
                                .OrderByDescending(x=>x.Id)
                                .FirstOrDefault();

                            if (lastMessage != null)
                            {
                                Guid dialogGUID = dialog.ConversationGuidId;
                                int InterlocutorID = context.ConversationGroup
                                    .Where(x => x.ConversationGuidId == dialogGUID &&
                                    x.UserId != userId).First().UserId;
                                BaseUser friendInterlocutor = UsWork.BaseUser(InterlocutorID);

                                int lastMessageUserId = lastMessage.UserId;
                                BaseUser lastSayUser = UsWork.BaseUser(lastMessageUserId);
                                var c = new ConversationModel();

                                c.LastMessageSendDate = lastMessage.SendingDate;
                                c.InterlocutorAvatar = Resize.ResizedAvatarUri(friendInterlocutor.AvatarResizeUri, ModTypes.c_scale, 100, 100);
                                c.InterlocutorName = friendInterlocutor.Name;
                                c.InterlocutorSurName = friendInterlocutor.SurName;

                                c.LastMessageSenderName = lastSayUser.Name;
                                c.LastMessageSenderSurName = lastSayUser.SurName;
                                if (lastMessage.Text.Length > 30)
                                    c.LastMessage = lastMessage.Text.Substring(0, 27) + "...";
                                else
                                    c.LastMessage = lastMessage.Text;
                                c.GuidId = conv.ConversationGuidId;
                                convs.Cnv.Add(c);
                            }
                        }
                    }
                }
                convs.Cnv = convs.Cnv.OrderByDescending(x => x.LastMessageSendDate).ToList();
            }
            return convs;
        }

        public NotShowedNews News(int userId)
        {
            var responce = new NotShowedNews();
            responce.NotReadedConversations = new Dictionary<string, int>();
            responce.NotReadedCryptoConversations = new Dictionary<string, int>();

            using (var context = new DataBaseContext())
            {
                List<Guid> userDisableDialogs = context.DisableDialogs
                    .Where(x=>x.UserDisablerId == userId)
                    .Select(x=>x.ConversationId)
                    .ToList();

                List<Guid> simpleDialogsGuids = context.ConversationGroup
                    .Where(x => x.UserId == userId)
                    .Select(x=>x.ConversationGuidId)
                    .ToList();

                List<Guid> cryptoDialogsGuids = context.SecretChatGroups
                    .Where(x => x.UserId == userId)
                    .Select(x => x.PartyGUID)
                    .ToList();

                if (simpleDialogsGuids.Count > 0)
                {
                    foreach (Guid dialog in simpleDialogsGuids)
                    {
                        Message[] notReaded = context.Messangers
                            .Where(x => x.UserId != userId)
                            .Where(x => x.IsReaded == false)
                            .Where(x => x.ConvarsationGuidId == dialog)
                            .Where(x => !userDisableDialogs.Contains(x.ConvarsationGuidId))
                            .ToArray();
                        if(notReaded.Length > 0)
                            responce.NotReadedConversations.Add(dialog.ToString(), notReaded.Length);
                    }
                }
                if (cryptoDialogsGuids.Count > 0)
                {
                    foreach(Guid cryptoDialog in cryptoDialogsGuids)
                    {
                        SecretMessages[] notReadedCrypto = context.SecretMessage
                            .Where(x => x.UserSender != userId)
                            .Where(x => x.IsReaded == false)
                            .Where(x => x.PartyId == cryptoDialog)
                            .Where(x => !userDisableDialogs.Contains(x.PartyId))
                            .ToArray();
                        if (notReadedCrypto.Length > 0)
                            responce.NotReadedCryptoConversations.Add(cryptoDialog.ToString(), notReadedCrypto.Length);
                    }


                }
                return responce;
            }
        }
    }
}