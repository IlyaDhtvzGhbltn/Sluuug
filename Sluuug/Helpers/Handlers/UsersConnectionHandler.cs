using Context;
using Slug.Context;
using Slug.Context.Tables;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers
{
    public class UsersConnectionHandler
    {
        public async Task AddConnection(string connectionID, string session, string ipAddress)
        {
            UsersHandler usersHandler = new UsersHandler();
            using (var context = new DataBaseContext())
            {
                CultureByIpChecker cultureDetecter = new CultureByIpChecker(ipAddress);
                string cultureCode = cultureDetecter.GetCulture().Name;

                var connectionItem = new UserConnections();
                connectionItem.ConnectionId = Guid.Parse( connectionID );
                connectionItem.OpenTime = DateTime.UtcNow;
                connectionItem.IsActive = true;
                connectionItem.UserId = usersHandler.UserIdBySession(session);
                connectionItem.IpAddress = ipAddress;
                connectionItem.CultureCode = cultureCode;

                context.UserConnections.Add(connectionItem);
                await context.SaveChangesAsync();
            }
        }

        public async Task CloseConnection(string session, string closedConnection)
        {
            var userHandler = new UsersHandler();
            var videoConferenceHandler = new VideoConferenceHandler();
            int userId = userHandler.UserIdBySession(session);
            var connectionId = Guid.Parse(closedConnection);
            using (var context = new DataBaseContext())
            {
                UserConnections connectionItem =  await
                    context.UserConnections.Where(
                    x =>
                    x.UserId == userId &&
                    x.IsActive == true &&
                    x.ConnectionId == connectionId
                    ).FirstOrDefaultAsync();
                int connectionsCount = context.UserConnections.Where(
                    x =>
                    x.UserId == userId &&
                    x.IsActive == true
                    ).Count();

                if (connectionItem != null)
                {
                    connectionItem.IsActive = false;
                    await context.SaveChangesAsync();
                }

                if (connectionsCount == 1)
                    await videoConferenceHandler.CloseAllConferencesUserExit(context, userId);
            }
        }

        public UserConnectionIdModel GetConnectionById(int userID)
        {
            using (var context = new DataBaseContext())
            {
                return getConnectionById(context, userID);
            }
        }

        public UserConnectionIdModel GetConnectionById(DataBaseContext context, int userID)
        {
            return getConnectionById(context, userID);
        }

        private UserConnectionIdModel getConnectionById(DataBaseContext context, int userID)
        {
            var userConnect = context.UserConnections
                .Where(x => x.UserId == userID && x.IsActive == true)
                .ToList();
            if (userConnect.Count != 0)
            {
                UserConnectionIdModel connections = new UserConnectionIdModel()
                {
                    ConnectionId = new List<string>(),
                    CultureCode = new List<string>()
                };
                userConnect.ForEach(x =>
                {
                    connections.ConnectionId.Add(x.ConnectionId.ToString());
                    connections.CultureCode.Add(x.CultureCode);
                });

                return connections;
            }
            else return null;
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
                foreach (int item in userID)
                {
                    var userConnections = context.UserConnections
                    .Where(x => x.UserId == item && x.IsActive == true)
                    .ToList();

                    foreach (var connection in userConnections)
                    {
                        connections.ConnectionId.Add(connection.ConnectionId.ToString());
                        connections.CultureCode.Add(connection.CultureCode);
                    }
                }
                return connections;
            }
        }

        public UserConnectionIdModel GetConnectionBySession(string sessionID)
        {
            var connections = new UserConnectionIdModel()
            {
                ConnectionId = new List<string>(),
                CultureCode = new List<string>()
            };

            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == sessionID);
                User user = context.Users.First(x => x.Id == session.UserId);

                List<UserConnections> userConnect = context.UserConnections
                    .Where(x => x.UserId == user.Id && x.IsActive == true)
                    .ToList();

                userConnect.ForEach(x=> 
                {
                    connections.ConnectionId.Add(x.ConnectionId.ToString());
                    connections.CultureCode.Add(x.CultureCode);
                });
                return connections;
            }
        }
    }
}