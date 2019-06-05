using Context;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers
{
    public class UsersConnectionHandler
    {
        public async Task AddConnection(string connectionID, string session, string ipAddress)
        {
            UsersHandler UW = new UsersHandler();
            using (var context = new DataBaseContext())
            {
                CultureByIpChecker cultureDetecter = new CultureByIpChecker(ipAddress);
                string cultureCode = cultureDetecter.GetCulture().Name;

                var connectionItem = new UserConnections();
                connectionItem.ConnectionID = Guid.Parse( connectionID );
                connectionItem.ConnectionTime = DateTime.UtcNow;
                connectionItem.ConnectionActiveStatus = true;
                connectionItem.UserID = UW.GetFullUserInfo(session).UserId;
                connectionItem.IpAddress = ipAddress;
                connectionItem.CultureCode = cultureCode;

                context.UserConnections.Add(connectionItem);
                await context.SaveChangesAsync();
            }
        }

        public void CloseConnection(string connectionID, string session)
        {
            UsersHandler UW = new UsersHandler();
            Guid guidID = Guid.Parse (connectionID);
            int userID = UW.GetFullUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                var connectionItem = context.UserConnections
                    .FirstOrDefault(x => x.ConnectionID == guidID && x.UserID == userID);
                connectionItem.ConnectionActiveStatus = false;
                context.SaveChanges();
            }
        }

        public UserConnectionIdModel GetConnectionById(int userID)
        {
            using (var context = new DataBaseContext())
            {
                var userConnect = context.UserConnections
                    .Where(x => x.UserID == userID && x.ConnectionActiveStatus == true)
                    .ToList();

                UserConnectionIdModel connections = new UserConnectionIdModel()
                {
                    ConnectionId = new List<string>(),
                    CultureCode = new List<string>()
                };
                userConnect.ForEach(x => 
                {
                    connections.ConnectionId.Add(x.ConnectionID.ToString());
                    connections.CultureCode.Add(x.CultureCode);
                });

                return connections;
            }
        }

        public UserConnectionIdModel GetConnectionsByIds(int[] userID)
        {
            UserConnectionIdModel connections = new UserConnectionIdModel()
            {
                ConnectionId = new List<string>(),
                CultureCode = new List<string>()
            };

            using (var context = new DataBaseContext())
            {
                var userConnect = new List<string>();
                foreach (int item in userID)
                {
                    var userConnections = context.UserConnections
                    .Where(x => x.UserID == item && x.ConnectionActiveStatus == true)
                    .ToList();

                    foreach (var connection in userConnections)
                    {
                        connections.ConnectionId.Add(connection.ConnectionID.ToString());
                        connections.CultureCode.Add(connection.CultureCode);
                    }
                }
                return connections;
            }
        }
    }
}