using Context;
using Slug.Context;
using Slug.Context.Dto.Settings;
using Slug.Context.Tables;
using Slug.Crypto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class SettingsHandler
    {


        public bool Change(string session, SetSettingsRequest newSettings)
        {
            var handler = new UsersHandler();
            string settingsChangeResult = string.Empty;

            UserSettings oldUserSett = handler.GetUserSettings(session);
            int userId = handler.UserIdBySession(session);

            if (oldUserSett.Email != newSettings.NewEmail)
            {
                if (newSettings.NewEmail != null)
                {
                    if (!newSettings.NewEmail.Contains("@"))
                    {
                        settingsChangeResult += "Invalid email format.";
                    }
                    else
                    {
                        ChangeEmail(userId, newSettings.NewEmail);
                    }
                }
            }

            if (oldUserSett.PasswordHash != newSettings.NewPassw)
            {
                if (newSettings.OldPasswRep != null && newSettings.NewPassw != null && newSettings.OldPassw != null)
                {
                    string oldPasswordHash = oldUserSett.PasswordHash;
                    string requesteOldPasswordHash = Converting.ConvertStringToSHA512(newSettings.OldPassw);

                    if (newSettings.OldPassw == newSettings.OldPasswRep && requesteOldPasswordHash == oldPasswordHash)
                    {
                        string requestedPasswordHash = Converting.ConvertStringToSHA512(newSettings.NewPassw);
                        ChangePassword(userId, requestedPasswordHash);
                    }
                    else if (newSettings.OldPassw != newSettings.OldPasswRep)
                    {
                        settingsChangeResult += settingsChangeResult + "Fields with old passwords do not match.";
                    }
                    else if (newSettings.OldPassw != oldPasswordHash)
                    {
                        settingsChangeResult += settingsChangeResult + "Old Password invalid.";
                    }

                }
            }
            if (newSettings.QuickMessageNeedChange)
            {
                ChangeQuickMessage(userId, newSettings.QuickMessage);
            }
            if (string.IsNullOrWhiteSpace(settingsChangeResult))
                return true;
            else return false;
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

        private void ChangeQuickMessage(int userID, bool flag)
        {
            using (var context = new DataBaseContext())
            {
                var settings = context.Users.First(x => x.Id == userID).Settings;
                settings.QuickMessage = flag;
                context.SaveChanges();
            }
        }
    }
}