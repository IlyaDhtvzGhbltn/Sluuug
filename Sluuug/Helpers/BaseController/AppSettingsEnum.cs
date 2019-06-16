using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers.BaseController
{
    public enum AppSettingsEnum
    {
        appSession,
        cloud,
        apiKey,
        apiSecret,

        smtpServerEmail,
        smtpServerPassword,
        smtpServerDisplayName,
        smtpHost,
        smtpPort,
        smtpSubjectConfirmRegister,
        smtpSubjectResetPassword
    }
}