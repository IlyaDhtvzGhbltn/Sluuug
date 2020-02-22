using Context;
using NLog;
using SharedModels.Enums;
using System;
using System.Collections.Generic;
using FakeBehaviorService.Messages;
using System.Linq;

namespace FakeBehaviorService
{
    class Program
    {
        static Random rnd = new Random();
        static Dictionary<int, bool> boolPars = new Dictionary<int, bool>()
        {
            { 0, false },
            { 1, true },
            { 2, false },
            { 3, false },
            { 4, false },
            { 5, false },
        };
        static Dictionary<bool, FriendshipItemStatus> boolRelation = new Dictionary<bool, FriendshipItemStatus>()
        {
            { false, FriendshipItemStatus.Pending },
            { true, FriendshipItemStatus.Accept }
        };

        static void Main(string[] args)
        {
            Logger logger = LogManager.GetLogger("scheduler_service_log");
            logger.Info("Scheduled Fake Service Started");
            using (var context = new DataBaseContext())
            {
                try
                {
                    var friendsInvitations = context.UserRelations
                        .Where(x => x.UserConfirmer.IsFakeBot == true && x.Status == FriendshipItemStatus.Pending).ToList();
                    if (friendsInvitations.Count > 0)
                        acceptFriendship(context, friendsInvitations);

                    var alreadyFriendsInvitations = context.UserRelations
                        .Where(x => x.UserConfirmer.IsFakeBot == true && x.Status == FriendshipItemStatus.Accept).ToList();
                    if (alreadyFriendsInvitations.Count > 0)
                        writeMessage(context, alreadyFriendsInvitations);
                }
                catch (Exception ex)
                {
                    Logger log = LogManager.GetLogger("internal_error_logger");
                    log.Error(ex);
                }
                finally
                {
                    logger.Info("Scheduled Fake Service Ended");
                }
            }
        }

        private static void acceptFriendship(DataBaseContext context, List<UsersRelation> relationsWithFake)
        {
            relationsWithFake.ForEach((relation)=> {
                int random = rnd.Next(0, 2);
                bool accept = boolPars[random];
                FriendshipItemStatus oldStatus = relation.Status;

                FriendshipItemStatus newStatus = boolRelation[accept];
                if (oldStatus == FriendshipItemStatus.Pending && newStatus == FriendshipItemStatus.Accept)
                    relation.Status = newStatus;
            });
            context.SaveChanges();
        }

        private static void writeMessage(DataBaseContext context, List<UsersRelation> relations)
        {
            relations.ForEach((relation) => 
            {
                List<Message> message = context.Messages.Where(x => x.UserId == relation.UserConfirmer.Id).ToList();
                Guid conversationId;
                if (message == null || message.Count == 0)
                {
                    conversationId = Guid.NewGuid(); 
                    context.Conversations.Add(new Conversation()
                    {
                        ConversationGuidId = conversationId,
                        CreatedDateTime = DateTime.UtcNow
                    });
                    context.ConversationGroup.AddRange(new List<ConversationGroup>()
                {
                    new ConversationGroup()
                    {
                        ConversationGuidId = conversationId,
                        UserId = relation.UserConfirmer.Id,
                    },
                    new ConversationGroup()
                    {
                        ConversationGuidId = conversationId,
                        UserId = relation.UserOferFrienshipSender.Id,
                    }
                });
                    bool send = boolPars[rnd.Next(0, 2)];
                    if (send)
                    {
                        string firstMsg = MessagesTemplate.First[rnd.Next(0, MessagesTemplate.First.Length + 1)];
                        insertMessage(context, conversationId, relation, firstMsg);
                    }
                }
                else
                {
                    conversationId = message[0].ConvarsationGuidId;
                    int count = message.Count;
                    switch (count)
                    {
                        case 0:
                            bool sendFirst = boolPars[rnd.Next(0, 2)];
                            if (sendFirst)
                            {
                                string firstMsg = MessagesTemplate.First[rnd.Next(0, MessagesTemplate.First.Length + 1)];
                                insertMessage(context, conversationId, relation, firstMsg);
                            }
                            break;
                        case 1:
                            bool sendSec = boolPars[rnd.Next(0, 5)];
                            if (sendSec)
                            {
                                string secondMsg = MessagesTemplate.Second[rnd.Next(0, MessagesTemplate.Second.Length + 1)];
                                insertMessage(context, conversationId, relation, secondMsg);
                            }
                            break;
                        case 2:
                            bool sendThir = boolPars[rnd.Next(0, 6)];
                            if (sendThir)
                            {
                                string thirdMsg = MessagesTemplate.Third[rnd.Next(0, MessagesTemplate.Third.Length + 1)];
                                insertMessage(context, conversationId, relation, thirdMsg);
                            }
                            break;
                    }
                }
            });
            context.SaveChanges();
        }

        private static void insertMessage(DataBaseContext context, Guid conversation, UsersRelation relation, string message)
        {
            context.Messages.Add(new Message()
            {
                ConvarsationGuidId = conversation,
                UserId = relation.UserConfirmer.Id,
                Text = message,
                SendingDate = DateTime.Now,
                IsReaded = false
            });
        }
    }
}
