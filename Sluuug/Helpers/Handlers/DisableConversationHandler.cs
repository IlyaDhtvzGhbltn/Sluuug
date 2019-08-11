using Context;
using Slug.Context.Tables;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Slug.Helpers.Handlers.HandlersInterface
{
    class DisableConversationHandler
    {
        public async Task DisableDialog(DataBaseContext context, Guid conversationId, int userId)
        {
            var disableDialogEntry = new DisableDialog()
            {
                ConversationId = conversationId,
                UserDisablerId = userId
            };
            context.DisableDialogs.Add(disableDialogEntry);
            await context.SaveChangesAsync();
        }

        public bool IsConversationDisabled(DataBaseContext context, Guid conversationId, int userId)
        {
            var disableEntry = context.DisableDialogs.FirstOrDefault(x =>
            x.ConversationId == conversationId &&
            x.UserDisablerId == userId
            );
            if (disableEntry != null)
                return true;
            else return false;
        }

        public async Task EnableDialog(DataBaseContext context, Guid conversationId)
        {
            var disableEntries = context.DisableDialogs.Where(x => x.ConversationId == conversationId).ToList();
            context.DisableDialogs.RemoveRange(disableEntries);
            await context.SaveChangesAsync();
        }

        public async Task EnableDialog(Guid conversationId)
        {
            using (var context = new DataBaseContext())
            {
                var disableEntries = context.DisableDialogs.Where(x => x.ConversationId == conversationId).ToList();
                context.DisableDialogs.RemoveRange(disableEntries);
                await context.SaveChangesAsync();
            }
        }
    }
}
