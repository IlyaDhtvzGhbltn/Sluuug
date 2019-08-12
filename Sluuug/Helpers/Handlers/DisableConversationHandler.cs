using Context;
using Slug.Context.Tables;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;

namespace Slug.Helpers.Handlers.HandlersInterface
{
    class DisableConversationHandler
    {
        public async Task DisableDialog(DataBaseContext context, Guid dialogId, int userId)
        {
            var disableDialogEntry = new DisableDialog()
            {
                ConversationId = dialogId,
                UserDisablerId = userId
            };
            context.DisableDialogs.Add(disableDialogEntry);
            await context.SaveChangesAsync();
            DialogType dialogType = await checkDialogType(context, dialogId);
            if (await isDialogNobodyNeed(context, dialogId, dialogType))
            {
                await deleteDialogPermamently(context, dialogId, dialogType);
            }
        }
        public bool IsConversationDisabled(DataBaseContext context, Guid dialogId, int userId)
        {
            var disableEntry = context.DisableDialogs.FirstOrDefault(x =>
            x.ConversationId == dialogId &&
            x.UserDisablerId == userId
            );
            if (disableEntry != null)
                return true;
            else return false;
        }
        public async Task EnableDialog(DataBaseContext context, Guid dialogId)
        {
            var disableEntries = context.DisableDialogs.Where(x => x.ConversationId == dialogId).ToList();
            context.DisableDialogs.RemoveRange(disableEntries);
            await context.SaveChangesAsync();
        }
        public async Task EnableDialog(Guid dialogId)
        {
            using (var context = new DataBaseContext())
            {
                var disableEntries = context.DisableDialogs.Where(x => x.ConversationId == dialogId).ToList();
                context.DisableDialogs.RemoveRange(disableEntries);
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsDialogBelongUser(DataBaseContext context, Guid dialogId, int userId)
        {
            var simpleChatEntry = await context.ConversationGroup.FirstOrDefaultAsync(x =>
                x.ConversationGuidId == dialogId &&
                x.UserId == userId);
            var cryptoChatEntry = await context.SecretChatGroups.FirstOrDefaultAsync(x =>
                x.PartyGUID == dialogId &&
                x.UserId == userId);
            if (simpleChatEntry != null || cryptoChatEntry != null)
                return true;
            else return false;
        }


        private async Task deleteDialogPermamently(DataBaseContext context, Guid dialogId, DialogType type)
        {
            var disableDialogEntries = context.DisableDialogs.Where(x => x.ConversationId == dialogId).ToList();
            if (type == DialogType.Simple)
            {
                var simpleDialogEntry = context.Conversations.First(x => x.ConversationGuidId == dialogId);
                context.Conversations.Remove(simpleDialogEntry);
                var simpleDialogGroupEntry = context.ConversationGroup.Where(x => x.ConversationGuidId == dialogId).ToList();
                context.ConversationGroup.RemoveRange(simpleDialogGroupEntry);
                var simpleDialogMessages = context.Messangers.Where(x => x.ConvarsationGuidId == dialogId).ToList();
                context.Messangers.RemoveRange(simpleDialogMessages);
            }
            else if (type == DialogType.Crypto)
            {
                var cryptoDialogEntry = context.SecretChat.First(x => x.PartyGUID == dialogId);
                context.SecretChat.Remove(cryptoDialogEntry);
                var cryptoDialogGroupEntry = context.SecretChatGroups.Where(x => x.PartyGUID == dialogId).ToList();
                context.SecretChatGroups.RemoveRange(cryptoDialogGroupEntry);
                var cryptoDialogMessages = context.SecretMessage.Where(x => x.PartyId == dialogId).ToList();
                context.SecretMessage.RemoveRange(cryptoDialogMessages);
            }
            context.DisableDialogs.RemoveRange(disableDialogEntries);
            await context.SaveChangesAsync();
        }
        private async Task<bool> isDialogNobodyNeed(DataBaseContext context, Guid dialogId, DialogType type)
        {
            int[] dialogDeleteParticipants = context.DisableDialogs
                .Where(x => x.ConversationId == dialogId)
                .Select(x => x.UserDisablerId)
                .ToArray();
            int[] dialogParticipants = new int[] { };

            if (type == DialogType.Simple) {
                 dialogParticipants = context.ConversationGroup
                    .Where(x => x.ConversationGuidId == dialogId)
                    .Select(x => x.UserId)
                    .ToArray();

            }
            else if (type == DialogType.Crypto)
            {
                dialogParticipants = context.SecretChatGroups
                   .Where(x => x.PartyGUID == dialogId)
                   .Select(x => x.UserId)
                   .ToArray();
            }
            if (dialogParticipants.Length == dialogDeleteParticipants.Length)
                return true;
            else return false;
        }

        private async Task<DialogType> checkDialogType(DataBaseContext context, Guid dialogId)
        {
            var simpleChat = context.Conversations.FirstOrDefaultAsync(x => x.ConversationGuidId == dialogId);
            if (simpleChat != null)
                return DialogType.Simple;
            else
            {
                var cryptoChat = context.SecretChat.FirstOrDefaultAsync(x => x.PartyGUID == dialogId);
                if (cryptoChat != null)
                    return DialogType.Crypto;
            }
            return DialogType.Undefined;
        }






        private enum DialogType
        {
            Undefined = 0,
            Simple = 1,
            Crypto = 2
        }
    }
}
