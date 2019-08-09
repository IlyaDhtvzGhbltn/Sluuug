using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.ServerError
{
    public static class ServerErrorCodesMessagesDictionary
    {
        public readonly static IDictionary<int, string> Error = new Dictionary<int, string>
        {
            { 3004, "❌Размер загружаемых файлов превышает допустимый лимит." }
        };
    }
}