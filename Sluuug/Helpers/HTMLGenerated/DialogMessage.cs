using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Slug.Helpers.HTMLGenerated
{
    public static class DialogMessage
    {
        public static string GenerateHtml(Model.DialogMessage model)
        {
            var sb = new StringBuilder();
            sb.Append("<div class='dialog_msg'>");
            sb.Append(string.Format("<img src = '{0}' height = '45' width = '45'/>", model.AvatarPath));
            sb.Append(string.Format("<span>{0}</span>", model.UserName));
            sb.Append(string.Format("<span>{0}</span>", model.SendTime));
            sb.Append(string.Format("<p>{0}</p>", model.Text));
            sb.Append(@"</div>");
            return sb.ToString();
        }
    }
}