using Context;
using Slug.Context;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public static class FriendshipChecker
    {
        public static bool IsUsersAreFriendsBySessionANDid(string sessionID, int userID)
        {
            var userHandler = new UsersHandler();
            var userInfo = userHandler.ProfileInfo(sessionID);
            using (var context = new DataBaseContext())
            {
                var friendShip = context.FriendsRelationship
                    .Where(x => x.UserOferFrienshipSender == userInfo.UserId || x.UserConfirmer == userInfo.UserId)
                    .Where(x => x.Status == FriendshipItemStatus.Accept)
                    .ToArray();
                if (friendShip.Count() >= 1)
                {
                    for (int i = 0; i < friendShip.Count(); i++)
                    {
                        if (friendShip[i].UserOferFrienshipSender == userID || friendShip[i].UserConfirmer == userID)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        public static bool IsUsersAreFriendsByConversationGuidANDid(Guid conversationID, int userID)
        {
            var dialogHandler = new UsersDialogHandler();


            int[] IDs = dialogHandler.GetConversatorsIds(conversationID);
            return CheckUsersFriendshipByIDs(IDs[0], IDs[1]);
        }

        public static bool CheckUsersFriendshipByIDs(int userFirst, int userSecond)
        {
            using (var context = new DataBaseContext())
            {
                var relation = context.FriendsRelationship
                    .FirstOrDefault(x => x.UserOferFrienshipSender == userFirst && x.UserConfirmer == userSecond ||
                    x.UserOferFrienshipSender == userSecond && x.UserConfirmer == userFirst);
                if (relation == null)
                    return false;
                else if (relation.Status == FriendshipItemStatus.Accept)
                    return true;
                else
                    return false;
            }
        }

        public static FriendsRelationship GetRelation(DataBaseContext context, int userFirst, int userSecond)
        {

            FriendsRelationship relation = context.FriendsRelationship
                .FirstOrDefault(x =>
                x.UserOferFrienshipSender == userFirst && x.UserConfirmer == userSecond ||
                x.UserConfirmer == userFirst && x.UserOferFrienshipSender == userSecond);
            return relation;
        }

        public static BlockedUsersEntries GetBlockRelation(DataBaseContext context, int userFirst, int userSecond)
        {
            BlockedUsersEntries relation = context.BlackList
                .FirstOrDefault(x => 
                x.UserBlocked == userFirst && x.UserBlocker == userSecond ||
                x.UserBlocked == userSecond && x.UserBlocker == userFirst);
            return relation;
        }
    }
}