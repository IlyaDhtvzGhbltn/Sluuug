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

        public async Task CloseConnection(string session)
        {
            UsersHandler userHandler = new UsersHandler();
            int userID = userHandler.UserIdBySession(session);

            using (var context = new DataBaseContext())
            {
                List<UserConnections> connectionItems =  await
                    context.UserConnections.Where(
                    x =>x.UserId == userID &&
                    x.IsActive == true
                    ).ToListAsync();

                if (connectionItems.Count > 0)
                {
                    foreach (var item in connectionItems)
                    {
                        item.IsActive = false;
                    }
                    await context.SaveChangesAsync();
                }
            }
        }

        public UserConnectionIdModel GetConnectionById(int userID)
        {
            using (var context = new DataBaseContext())
            {
                var userConnect = context.UserConnections
                    .Where(x => x.UserId == userID && x.IsActive == true)
                    .ToList();

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