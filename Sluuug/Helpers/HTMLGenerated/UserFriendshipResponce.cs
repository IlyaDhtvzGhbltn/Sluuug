using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Slug.Helpers.HTMLGenerated
{
    public static class UserFriendshipResponce
    {
        public static string GenerateHtml(string culture)
        {
            CultureInfo cul = CultureInfo.CreateSpecificCulture(culture);

            StringBuilder sb = new StringBuilder();
            string message = Properties.Resources.ResourceManager.GetString("Text_Invite_To_Friendship", cul);
            sb.AppendFormat("<div id='send_invite'><span>{0}</span></div>", message);
            return sb.ToString();
        }
    }
}