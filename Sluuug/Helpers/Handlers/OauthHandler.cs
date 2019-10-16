using Context;
using Slug.Context.Dto.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers.Handlers
{
    public class OauthHandler
    {
        public async Task SaveTokenEntry(VkToken token)
        {
            using (var context = new DataBaseContext())
            {
                context.VkOAuthTokens.Add(new Context.Tables.VkOAuthToken()
                {
                   Code = token.Code,
                   Token = token.Token,
                   VkUserId = token.VkUserId,
                   ReceivedDate = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
            }
        }

        public bool RegisterNewVkUser()
        {

            return false;
        }
    }
}