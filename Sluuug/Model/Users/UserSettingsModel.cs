using Slug.Context.Dto.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class UserSettingsModel
    {
        public NotificationTypes NotifyType { get; set; }

        public string Email { get; set; }

        public bool QuickMessage { get; set; }
    }
}