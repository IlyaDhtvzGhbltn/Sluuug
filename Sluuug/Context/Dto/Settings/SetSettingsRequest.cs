using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Settings
{
    public class SetSettingsRequest
    {
        public int NotifyType { get; set; }

        public string NewEmail { get; set; }

        public string OldPassw { get; set; }

        public string OldPasswRep { get; set; }

        public string NewPassw { get; set; }

        public bool QuickMessage { get; set; }
        public bool QuickMessageNeedChange { get; set; }
    } 
}