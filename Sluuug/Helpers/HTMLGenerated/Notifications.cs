using Context;
using Slug.Context.Dto.Messages;
using Slug.Context.Dto.Notification;
using Slug.ImageEdit;
using Slug.Model;
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
            };

        private static readonly IReadOnlyDictionary<NotificationType, string> typeToLink =
            new Dictionary<NotificationType, string>()
            {
                        { NotificationType.NewMessage, "/private/cnv" },
                        { NotificationType.NewInviteVideoConference, "/private/invite_video_conversation" },
                        { NotificationType.NewInviteSecretChat, "/private/crypto_cnv" },
                        { NotificationType.NewMessageSecret, "/private/crypto_cnv" },
                        { NotificationType.AcceptYourInviteSecretChat, "/private/crypto_cnv" },
                        { NotificationType.NewInviteFriendship, "/private/contacts" },
                        { NotificationType.AcceptFriendship, "/private/contacts" },
            };

        public static string GenerateHtml(NotificationType type, CutUserInfoModel model, string culture)
        {
            StringBuilder sb = new StringBuilder();
            CultureInfo cul = CultureInfo.CreateSpecificCulture(culture);
            string mess = Properties.Resources.ResourceManager.GetString(typeToResx[type], cul);
            sb.AppendFormat("<span>{0} {1} {2}</span>", mess, model.Name, model.SurName);
            string uriAvatar = Resize.ResizedUri(model.AvatarUri, ModTypes.c_scale, 30);
            sb.AppendFormat("<img src='{0}' height='30' width='30' />", uriAvatar);
            string uriMore = typeToLink[type];
            string click = Properties.Resources.ResourceManager.GetString("Text_Click_Here_Details", cul);
            sb.AppendFormat("<p><a href='{0}'>{1}</a></p>", uriMore, click);
            return sb.ToString();
        }
    }


}