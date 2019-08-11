using Context;
using Slug.Context.Dto.Messages;
using Slug.Context.Dto.Notification;
using Slug.ImageEdit;
using Slug.Model;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Slug.Helpers.HTMLGenerated
{
    public static class Notifications
    {
        private static readonly IReadOnlyDictionary<NotificationType, string> typeToResx =
            new Dictionary<NotificationType, string>()
            {
                { NotificationType.NewMessage, "Notify_New_Message" },
                { NotificationType.NewInviteVideoConference, "Notify_New_VideoConference_Invite" },
                { NotificationType.NewInviteSecretChat, "Notify_New_Secret_Chat" },
                { NotificationType.NewMessageSecret, "Notify_New_Message_Secret" },
                { NotificationType.AcceptYourInviteSecretChat, "Notify_Accept_Your_Invite_Secret" },
                { NotificationType.NewInviteFriendship, "Notify_Friendship_Invite" },
                { NotificationType.AcceptFriendship, "Notify_Accept_Your_Friendship" },
                { NotificationType.CryptoInviteRefuse, "Notify_Crypto_Invitation_Refused" }
            };

        private static readonly IReadOnlyDictionary<NotificationType, string> typeToLink =
            new Dictionary<NotificationType, string>()
            {
                    { NotificationType.NewMessage, "/private/cnv" },
                    { NotificationType.NewInviteVideoConference, "/private/invite_video_conversation?type=in" },
                    { NotificationType.NewInviteSecretChat, "/private/crypto_cnv?type=in" },
                    { NotificationType.NewMessageSecret, "/private/crypto_cnv?type=accept" },
                    { NotificationType.AcceptYourInviteSecretChat, "/private/crypto_cnv?type=accept" },
                    { NotificationType.NewInviteFriendship, "/private/contacts?type=in" },
                    { NotificationType.AcceptFriendship, "/private/contacts?type=accept" },
                    { NotificationType.CryptoInviteRefuse, "/private/crypto_cnv" }
            };

        public static string GenerateHtml(NotificationType type, BaseUser model, string culture)
        {
            StringBuilder sb = new StringBuilder();
            CultureInfo cul = CultureInfo.CreateSpecificCulture(culture);

            sb.Append("<div class='notify-avatar-container'>");
                string uriAvatar = Resize.ResizedAvatarUri(model.AvatarResizeUri, ModTypes.c_scale, 50, 50);
                sb.AppendFormat("<img onclick='redirectToUser('{0}')' src='{1}' />", model.UserId, uriAvatar);
            sb.Append("</div>");
            sb.Append("<div class='notify-message-container'>");
                sb.Append("<div class='notify-message-header'>");
                    string mess = Properties.Resources.ResourceManager.GetString(typeToResx[type], cul);
                    sb.AppendFormat("<span>{0} <span class='user-notify-name' onclick='redirectToUser({1})'>{2} {3}</span></span>", mess, model.UserId, model.Name, model.SurName);
                sb.Append("</div>");
                sb.Append("<div class='notify-message-body'>");
                    string uriMore = typeToLink[type];
                    sb.Append("<span class='notify-about-span'>нажмите, что бы узнать</span>");
                    sb.AppendFormat("<a class='notify-about-a' href='{0}'>подробнее</a>", uriMore);
                sb.Append("</div>");
            sb.Append("</div>");


            return sb.ToString();
        }
    }


}