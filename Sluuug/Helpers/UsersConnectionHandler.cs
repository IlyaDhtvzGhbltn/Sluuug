using Context;
using Slug.Context;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class UsersConnectionHandler
    {
        public void AddConnection(string connectionID, string session)
        {
            UsersHandler UW = new UsersHandler();
            using (var context = new DataBaseContext())
            {
                var connectionItem = new UserConnections();
                connectionItem.ConnectionID = Guid.Parse( connectionID );
                connectionItem.ConnectionTime = DateTime.UtcNow;
                connectionItem.ConnectionActiveStatus = true;
                connectionItem.UserID = UW.GetUserInfo(session).UserId;

                context.UserConnections.Add(connectionItem);
                context.SaveChanges();
            }
        }

        public void CloseConnection(string connectionID, string session)
        {
            UsersHandler UW = new UsersHandler();
            Guid guidID = Guid.Parse (connectionID);
            int userID = UW.GetUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                var connectionItem = context.UserConnections
                    .FirstOrDefault(x => x.ConnectionID == guidID && x.UserID == userID);
                connectionItem.ConnectionActiveStatus = false;
                context.SaveChanges();
            }
        }

        public IList<string> GetConnectionById(int userID)
        {
            using (var context = new DataBaseContext())
            {
                IList<string> userConnect = context.UserConnections
                    .Where(x => x.UserID == userID && x.ConnectionActiveStatus == true)
                    .Select(x => x.ConnectionID.ToString())
                    .ToArray();
                return userConnect;
            }
        }

        public IList<string> GetConnectionsByIds(int[] userID)
        {
            using (var context = new DataBaseContext())
            {
                IList<string> userConnect = new List<string>();
                foreach (int item in userID)
                {
                    string[] connections = context.UserConnections
                    .Where(x => x.UserID == item && x.ConnectionActiveStatus == true)
                    .Select(x => x.ConnectionID.ToString())
                    .ToArray();

                    foreach (string connection in connections)
                    {
                        userConnect.Add(connection);
                    }
                }
                return userConnect;
            }
        }
    }
}