using Slug.Model.Messager.CryptoChat;
using System.Collections.Generic;
using Slug.Model.Users;
using Slug.Model.Users.Relations;

namespace Slug.Model
{
    public class CryptoConversationGroupModel
    {
        public List<CryptoConversationModel> CurrentActiveChats { get; set; }
        public List<CryptoConversationModel> OutCommingInviters { get; set; }
        public List<CryptoConversationModel> IncommingInviters { get; set; }

        public List<CryptoDialogUser> FriendsICanInvite { get; set; }
    }
}