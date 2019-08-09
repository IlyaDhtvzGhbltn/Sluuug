using Slug.Model.Messager.CryptoChat;
using System.Collections.Generic;
using Slug.Model.Users;

namespace Slug.Model
{
    public class CryptoConversationGroupModel
    {
        public List<CryptoConversationModel> CurrentActiveChats { get; set; }
        public List<CryptoConversationModel> OutCommingInviters { get; set; }
        public List<CryptoConversationModel> IncommingInviters { get; set; }

        public List<BaseUser> FriendsICanInvite { get; set; }
    }
}