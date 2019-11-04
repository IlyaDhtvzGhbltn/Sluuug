using System;
using Context;
using Slug.Crypto;
using System.Linq;
using Slug.Context.Tables;
using System.Data.Entity.Validation;

namespace Slug.Context
{
    public class SessionsHandler
    {

        public string OpenSession(SessionTypes type, int userId)
        {
            string sessionNumber = string.Empty;
            sessionNumber = Crypto.Encryption.EncryptionStringtoMD5(DateTime.Now.Ticks.ToString());
            using (var context = new DataBaseContext())
            {

                var session = new Session();
                session.DateStart = DateTime.UtcNow;
                session.Expired = false;
                session.Number = sessionNumber;
                session.Type = (int)type;
                session.UserId = userId;
                context.Sessions.Add(session);
                context.SaveChanges();
            }

            return sessionNumber;
        }

        public int GetSessionId(string number, SessionTypes type)
        {
            int result = -1;
            using (var context = new DataBaseContext())
            {
                Session exist = context.Sessions.Where(x => x.Number == number)
                                                .Where(x => x.Type == (int)type)
                                                .FirstOrDefault();
                if (exist != null)
                    result = exist.Id;
            }
            return result;
        }

        public SessionTypes GetSessionType(string number)
        {
            SessionTypes type = SessionTypes.Guest;
            using (var context = new DataBaseContext())
            {
                Session sess = context.Sessions.Where(x => x.Number == number).FirstOrDefault();
                if (sess != null)
                    type = (SessionTypes)context.Sessions.FirstOrDefault(x => x.Number == number).Type;
            }
            return type;
        }

        public void CloseSession(string number)
        {
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == number);
                session.Type = (int)SessionTypes.Exit;
                session.Expired = true;
                context.SaveChanges();
            }
        }
    }
}