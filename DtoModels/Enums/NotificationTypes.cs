using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedModels.Enums
{
    public enum NotificationType
    {
        NewMessage = 0,
        NewInviteVideoConference = 1,
        NewInviteSecretChat = 2,
        NewMessageSecret = 3,
        AcceptYourInviteSecretChat = 4,
        NewInviteFriendship = 5,
        AcceptFriendship = 6,
        CryptoInviteRefuse = 7
    }
}