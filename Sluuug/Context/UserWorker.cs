using Context;
using Slug.Context.Tables;
using Slug.Crypto;
using Slug.Model;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace Slug.Context
{
    public class UserWorker
    {
        public UserConfirmationDitails RegisterNew(RegisteringUserModel user)
        {
            string activationSessionId = string.Empty;
            string activationMailParam = string.Empty;

            using (var context = new DataBaseContext())
            {
                var newUser = new Context.Tables.User();

                newUser.CountryCode = user.CountryCode;
                newUser.DateOfBirth = user.DateBirth;
                newUser.Email = user.Email;
                newUser.Name = user.Name;
                newUser.ForName = user.ForName;
                newUser.Login = user.Login;
                newUser.Password = Security.ConvertStringToSHA512(user.PasswordHash);

                newUser.UserStatus = (int)UserStatuses.AwaitConfirmation;

                context.Users.Add(newUser);
                var sesWk = new SessionWorker();
                activationSessionId = sesWk.OpenSession(SessionTypes.AwaitEmailConfirm, 0);
                try
                {
                    context.SaveChanges();
                    var linkMail = new ActivationMailWorker();
                    activationMailParam = linkMail.CreateActivationEntries(context.Users.First(x => x.Email == user.Email).Id);
                    context.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {

                }
            }
            return new UserConfirmationDitails { ActivatioMailParam = activationMailParam, ActivationSessionId = activationSessionId };
        }

        public void ConfirmUser(int id)
        {
            using (var context = new DataBaseContext())
            {
                var user = context.Users.Where(x => x.Id == id).First();
                user.UserStatus = (int)UserStatuses.Active;
                context.SaveChanges();
            }
        }

        public int VerifyUser(string login, string hashPassword)
        {
            string savedPassword = Crypto.Security.ConvertStringToSHA512(hashPassword);
            using (var dbContext = new DataBaseContext())
            {
                var user = dbContext.Users.Where(x => x.Login == login && x.Password == savedPassword).FirstOrDefault();
                if (user != null)
                    return user.Id;
            }
            return 0;
        }

        public CutUserInfoModel UserInfo(int id)
        {
            return new CutUserInfoModel();
        }

        public CutUserInfoModel GetUserInfo(string session_id)
        {
            var userModel = new CutUserInfoModel();
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == session_id);
                var user = context.Users.First(x => x.Id == session.UserId);
                userModel.Name = user.Name;
                userModel.ForName = user.ForName;
                userModel.Sity = user.Sity;
                userModel.MetroStation = user.MetroStation;
                userModel.DateBirth = user.DateOfBirth;
                userModel.AvatarUri = "/resources/img/flag_usa.png";
            }
            return userModel;
        }

        public class UserConfirmationDitails
        {
            public string ActivationSessionId { get; set; }
            public string ActivatioMailParam { get; set; }
        }
    }
}