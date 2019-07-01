using Slug.Context.Dto.Notification;
using Slug.Model.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers.Handlers
{
    public class NotificationHandler
    {
        public void SaveNotification(NotificationType types, int userID, int userSenderID, string message = null)
        {

        }

        public UserNotivication[] GetUserNotificationHistory(string session, int offset = 1)
        {
            return null;
        }
    }
}