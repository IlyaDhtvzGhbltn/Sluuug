using Slug.ImageEdit;
using Slug.Model.VideoConference;
using Sluuug;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;

namespace Slug.Helpers.HTMLGenerated
{
    public static class VideoConferenceInviteToRedirect
    {
        public static string GenerateHtml(IncomingInviteModel model, string culture)
        {
            string incommingICO = Resize.ResizedUri("https://res.cloudinary.com/dlk1sqmj4/image/upload/v1553527278/incomming_call.png", ModTypes.c_scale, 45);
            CultureInfo cul = CultureInfo.CreateSpecificCulture(culture);
            string mess = Properties.Resources.ResourceManager.GetString("Text_Call_To_You", cul);

            var sb = new StringBuilder();
            sb.AppendFormat("<div class='incomming_call' id='called__{0}'>", model.InviterID);
            sb.AppendFormat("<img src='{0}'/>", incommingICO);
            sb.AppendFormat("<img src='{0}'/>", model.AvatarUri);
            sb.AppendFormat("<span>{0} {1} {2}</span>", model.CallerName, model.CallerSurName, mess);
            sb.AppendFormat("</div>");
            return sb.ToString();
        }
    }
}