using SharedModels.Enums;
using Slug.Model.Notifications;

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