using System;
using System.Collections.Generic;
using System.Data.Entity;
using Slug.Context.Tables;

namespace Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=DBConnection")
        {
            Database.Initialize(false);
        }

        public virtual DbSet<ActivationLink> ActivationLink { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SessionType> SessionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInfo> UsersInfos { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }

        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<ConversationGroup> ConversationGroup { get; set; }
        public virtual DbSet<Message> Messangers { get; set; }
        public virtual DbSet<Avatars> Avatars { get; set; }
        public virtual DbSet<FriendsRelationship> FriendsRelationship { get; set; }
        public virtual DbSet<SecretMessages> SecretMessage { get; set; }
        public virtual DbSet<SecretChat> SecretChat { get; set; }
        public virtual DbSet<SecretChatGroup> SecretChatGroup { get; set; }
        public virtual DbSet<VideoConference> VideoConferences { get; set; }
        public virtual DbSet<VideoConferenceGroups> VideoConferenceGroups { get; set; }

        public virtual DbSet<UserConnection> UserConnections { get; set; }
    }
}
