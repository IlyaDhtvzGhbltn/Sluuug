using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Notification
{
    public enum NotificationType
    {
        NewMessage = 0,
        NewInviteVideoConference,
        NewInviteSecretChat,
        NewMessageSecret,
        AcceptYourInviteSecretChat,
        NewInviteFriendship,
        AcceptFriendship
    }
}