using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Context;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.Messager.SimpleChat;
using System.Globalization;
using Slug.Context.Dto.Conversation;
using Slug.Model.Users;
using SharedModels.Users;

namespace Slug.Helpers
{
    public class UsersDialogHandler
    {
        private UsersHandler UserWorker = new UsersHandler();
        private readonly int messOnPage = int.Parse(WebAppSettings.AppSettings[AppSettingsEnum.messagesOnPage.ToString()]);
                      
        public DialogModel GetMessanges(Guid convId, int userID, int page)
        {
            if (page <= 0)
                page = 1;

            var dModel = new DialogModel();
            var messangs = new List<MessageModel>();
            var userInfos = new Dictionary<int, BaseUser>();

            using (var context = new DataBaseContext())
            {
                int multipleCount = context.Messangers
                                        .Where(x => x.ConvarsationGuidId == convId)
                                        .Count();

                decimal del = ( (decimal)multipleCount / messOnPage);
                int resMultiple = Convert.ToInt32( Math.Ceiling(del) );
                if (page > resMultiple)
                    page = resMultiple;

                dModel.PagesCount = resMultiple;
                int interlocutorID = context.ConversationGroup
                    .First(x => x.ConversationGuidId == convId && x.UserId != userID).UserId;
                var interlocutorUser = context.Users.First(x => x.Id == interlocutorID).UserFullInfo;
                var userHandler = new UsersHandler();
                dModel.OwnResizeAvatar = userHandler.BaseUser(userID).SmallAvatar;
                dModel.Interlocutor = string.Format("{0} {1}", interlocutorUser.Name, interlocutorUser.SurName);

                List<Message> msgs = context.Messangers
                    .Where(x => x.ConvarsationGuidId == convId)
                    .OrderBy(x=>x.Id)
                    .Skip( (resMultiple - page) * messOnPage)
                    .Take( messOnPage)
                    .ToList();

                msgs.ForEach(message => 
                {
                    var msg = new MessageModel();

                    if (!userInfos.ContainsKey(message.UserId))
                        userInfos[message.UserId] = UserWorker.BaseUser(message.UserId, context);
                    if(message.UserId != userID)
                        msg.IsIncomming = true;

                    msg.Text = message.Text;
                    msg.SendTime = message.SendingDate.ToString("HH:mm", new CultureInfo("ru-RU"));
                    msg.ConversationId = message.ConvarsationGuidId;
                    msg.AvatarPath = userInfos[message.UserId].SmallAvatar;
                    msg.UserName = userInfos[message.UserId].Name;
                    msg.UserSurname = userInfos[message.UserId].SurName;
                    msg.Text = message.Text;
                    msg.SenderId = message.UserId;
                    messangs.Add(msg);
                });
                dModel.Messages = messangs;

                var notReadMessage = context.Messangers
                    .Where(x =>
                x.ConvarsationGuidId == convId &&
                x.UserId == interlocutorID)
                .ToList();

                notReadMessage.ForEach(x =>
                x.IsReaded = true);

                context.SaveChanges();
            }
            return dModel;
        }

        public MoreMessegesDialogResponce GetMoreMessages(MoreMessagesDialogRequest request, int requestSenderUserId)
        {
            using (var context = new DataBaseContext())
            {
                var userInfos = new Dictionary<int, BaseUser>();
                var query = context.Messangers
                    .Where(ms => ms.ConvarsationGuidId == request.DialogId);

                List<Message> messages = query
                    .OrderByDescending(x=>x.SendingDate)
                    .Skip(request.LoadedMessages)
                    .Take(messOnPage)
                    .ToList();

                var resp = new MoreMessegesDialogResponce();
                resp.DialogMessageCount = query.Count();
                resp.Messages = new List<MessageModel>();


                messages.ForEach(msg => 
                {
                    if(!userInfos.ContainsKey(msg.UserId))
                        userInfos[msg.UserId] = UserWorker.BaseUser(msg.UserId, context);

                    bool incomm = requestSenderUserId == msg.UserId ? false : true;
                    resp.Messages.Add(new MessageModel()
                    {
                        IsIncomming = incomm,
                        AvatarPath = userInfos[msg.UserId].SmallAvatar,
                        UserName = userInfos[msg.UserId].Name,
                        UserSurname = userInfos[msg.UserId].SurName,
                        SenderId = userInfos[msg.UserId].UserId,
                        Text = msg.Text,
                        ConversationId = msg.ConvarsationGuidId,
                        SendTime = msg.SendingDate.ToString("f", new CultureInfo("ru-RU"))
                    });
                });
                return resp;
            }
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
        public async Task SaveMsg(Guid convId, int userSenderId, string text)
        {
            using (var context = new DataBaseContext())
            {
                var msg = new Message();
                msg.ConvarsationGuidId = convId;
                msg.Text = text;
                msg.UserId = userSenderId;
                msg.SendingDate = DateTime.Now;
                msg.IsReaded = false;
                context.Messangers.Add(msg);

                await context.SaveChangesAsync();
            }
        }
    }
}