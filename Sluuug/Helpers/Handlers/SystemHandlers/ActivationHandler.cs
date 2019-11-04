using Context;
using Slug.Context.Tables;
using Slug.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context
{
    public class ActivationHandler
    {
        public string CreateActivationEntries(int userId)
        {
            string activateParam = Crypto.Encryption.EncryptionStringtoMD5(DateTime.Now.Ticks.ToString());
            using (var context = new DataBaseContext())
            {
                var activationMailLink = new ActivationLink();
                activationMailLink.ActivationParam = activateParam;
                activationMailLink.UserId = userId;
                activationMailLink.IsExpired = false;

                context.ActivationLink.Add(activationMailLink);
                context.SaveChanges();
            }

            return activateParam;
        }

        public ActivationLnkStatus GetLinkStatus(string activate_id)
        {
            using (var context = new DataBaseContext())
            {
                var activLink = context.ActivationLink.Where(x => x.ActivationParam == activate_id).FirstOrDefault();
                if (activLink == null || activLink.IsExpired == true)
                    return ActivationLnkStatus.Fail;
                else if (activLink.ActivationParam == activate_id)
                    return ActivationLnkStatus.Correct;
            }
            return ActivationLnkStatus.Fail;
        }

        public ActivationLink GetActivateLink(string activate_id)
        {
            ActivationLink user = null;
            using (var context = new DataBaseContext())
            {
                ActivationLink link = context.ActivationLink.FirstOrDefault(x => x.ActivationParam == activate_id);
                if (link != null)
                {
                    user = link;
                }
            }
            return user;
        }

        public void CloseActivationEntries(int id)
        {
            using (var context = new DataBaseContext())
            {
                var activationLnk = context.ActivationLink.First(x => x.Id == id);
                activationLnk.IsExpired = true;
                context.SaveChanges();
            }
        }

    }
}