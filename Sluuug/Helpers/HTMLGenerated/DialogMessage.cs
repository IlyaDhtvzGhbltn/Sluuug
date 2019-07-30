using Slug.ImageEdit;
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
            sb.Append(string.Format("<img src = '{0}'/>", Resize.ResizedAvatarUri(model.AvatarPath, ModTypes.c_scale, 100, 100)));
            sb.Append(string.Format("<h2>{0} {1}</h2>", model.UserName, model.UserSurname));
            sb.Append(string.Format("<p>{0}</p>", model.Text));
            return sb.ToString();
        }
    }
}