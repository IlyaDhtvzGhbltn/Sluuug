using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Slug.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Translate(this HtmlHelper htmlHelper, string key)
        {
            var cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            string value = Properties.Resources.ResourceManager.GetString(key, cultureInfo);
            return MvcHtmlString.Create(value);
        }
    }
}