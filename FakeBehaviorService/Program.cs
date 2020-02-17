using Context;
using NLog;
using SharedModels.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FakeBehaviorService
{
    class Program
    {
        static Random rnd = new Random();
        static Dictionary<int, bool> boolPars = new Dictionary<int, bool>()
        {
            { 0, false },
            { 1, true }
        };
        static Dictionary<bool, FriendshipItemStatus> boolRelation = new Dictionary<bool, FriendshipItemStatus>()
        {
            { false, FriendshipItemStatus.Pending },
            { true, FriendshipItemStatus.Accept }
        };

        static void Main(string[] args)
        {
            Logger logger = LogManager.GetLogger("information");
            logger.Info("Scheduled Fake Service Started");
            using (var context = new DataBaseContext())
            {
                var friendsInvitations = context.UserRelations
                    .Where(x=>x.UserConfirmer.IsFakeBot == true && x.Status == FriendshipItemStatus.Pending).ToList();
                if (friendsInvitations.Count > 0)
                    acceptFriendship(context, friendsInvitations);
            }
        }

        private static void acceptFriendship(DataBaseContext context, List<UsersRelation> relationsWithFake)
        {
            relationsWithFake.ForEach((relation)=> {
                int random = rnd.Next(0, 2);
                bool accept = boolPars[random];
                FriendshipItemStatus newStatus = boolRelation[accept];
                if(newStatus == FriendshipItemStatus.Accept)
                    relation.Status = newStatus;
            });
            context.SaveChanges();
        }
    }
}
