using Context;
using Slug.Context;
using Slug.Context.Dto.Settings;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class SettingsHandler
    {


        public Tuple<bool,string> Change(string session, SetSettingsRequest newSettings)
        {
            var handler = new UsersHandler();
            string settingsChangeResult = string.Empty;

            UserSettings currentUserSett = handler.GetUserSettings(session);
            int userID = handler.GetUserInfo(session).UserId;

            if (currentUserSett.Email != newSettings.NewEmail)
            {
                if (newSettings.NewEmail != null)
                {
                    if (!newSettings.NewEmail.Contains("@"))
                    {
                        settingsChangeResult += "Invalid email format.";
                    }
                    else
                    {
                        ChangeEmail(userID, newSettings.NewEmail);
                        settingsChangeResult += "Email adress successfully changed.";
                    }
                }
            }
            if ((int)currentUserSett.NotificationType != newSettings.NotifyType)
            {
                ChangeNotify(userID, (NotificationTypes)newSettings.NotifyType);
                settingsChangeResult += Environment.NewLine +  "Notification Type successfully changed.";
            }
            if (currentUserSett.PasswordHash != newSettings.NewPassw)
            {
                if (newSettings.NewPassw != null && newSettings.OldPassw != null && newSettings.OldPasswRep != null)
                {
                    string oldPass = currentUserSett.PasswordHash;
                    if (newSettings.OldPassw != newSettings.OldPasswRep)
                    {
                        settingsChangeResult += Environment.NewLine + "Fields with old passwords do not match.";
                    }
                    else if (newSettings.OldPassw == newSettings.OldPasswRep && newSettings.OldPassw != oldPass)
                    {
                        settingsChangeResult += Environment.NewLine + "Old Password invalid.";
                    }
                    else if (newSettings.OldPassw == newSettings.OldPasswRep && newSettings.OldPassw == oldPass)
                    {
                        ChangePassword(userID, newSettings.NewPassw);
                        settingsChangeResult += Environment.NewLine + "Password successfully changed.";
                    }
                }
            }
            else {
                return new Tuple<bool, string>(false, "Is no changes in settings");
            }

            return new Tuple<bool, string>(true, settingsChangeResult);
        }

        private void ChangePassword(int userID, string newpassword)
        {
            using (var context = new DataBaseContext())
            {
                var settings = context.Users.First(x => x.Id == userID).Settings;
                settings.PasswordHash = newpassword;
                context.SaveChanges();
            }
        }

        private void ChangeNotify(int userID, NotificationTypes newType)
        {
            using (var context = new DataBaseContext())
            {
                var settings = context.Users.First(x => x.Id == userID).Settings;
                settings.NotificationType = newType;
                context.SaveChanges();
            }
        }

        private void ChangeEmail(int userID, string newEmail)
        {
            using (var context = new DataBaseContext())
            {
                var settings = context.Users.First(x => x.Id == userID).Settings;
                settings.Email = newEmail;
                context.SaveChanges();
            }
        }
    }
}