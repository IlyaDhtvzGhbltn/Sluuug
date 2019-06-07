using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Notify
{
    public enum NotificationType
    {
        NewMessage =0,
        NewInviteVideoConference,
        NewInviteSecretChat,
        NewMessageSecret,
        AcceptYourInviteToSecret,
        NewInviteFriendship,
        AcceptFriendship
    }
}