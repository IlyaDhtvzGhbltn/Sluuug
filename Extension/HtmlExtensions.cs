using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Slug.Extension
{
    public static class HtmlExtensions
    {
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
            #if DEBUG
                return true;
            #else
                  return false;
            #endif
        }
    }
}
