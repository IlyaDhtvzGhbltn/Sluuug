using Slug.Context.Dto.CryptoConversation;
using Slug.ImageEdit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

namespace Slug.Helpers.HTMLGenerated
{
    public static class InviteToSecretConversation
    {
        public static string GenerateHtml(PublicDataCryptoConversation model, string culture)
        {
            CultureInfo cul = CultureInfo.CreateSpecificCulture(culture);
            string openingDateText = Properties.Resources.ResourceManager.GetString("Text_Opening_Date", cul);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<span>{0} '{1}' </span>", openingDateText, model.CreationDate);
            string textStatus = Properties.Resources.ResourceManager.GetString("Text_Secret_Chat_Status", cul);
            string status = Properties.Resources.ResourceManager.GetString("Text_Status_Active", cul);
            sb.AppendFormat("<span>{0} {1}</span>", textStatus, status);
            string textInviter = Properties.Resources.ResourceManager.GetString("Text_Inviter", cul);
            sb.AppendFormat("<p><span>{0} {1}</span></p>", textInviter, model.CreatorName);
            sb.AppendFormat("<img alt='avatar' src='{0}'>", Resize.ResizedAvatarUri(model.CreatorAvatar, ModTypes.c_scale, 60, 60));
            string accept = Properties.Resources.ResourceManager.GetString("Button_Accept_Invite", cul);
            sb.AppendFormat("<button onclick='accept_invite(this)' id='{0}' />{1}</button>", model.ConvGuidId, accept);
            return sb.ToString();
        }
    }
}