﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using Slug.Context.Dto;
using Slug.Context.Dto.UserFullInfo;
using Slug.Context.Tables;
using Slug.DbInitialisation;

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
        public virtual DbSet<UserInfo> UsersInfo { get; set; }

        public virtual DbSet<UserSettings> UserSettings { get; set; }

        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<ConversationGroup> ConversationGroup { get; set; }
        public virtual DbSet<Message> Messangers { get; set; }
        public virtual DbSet<Avatars> Avatars { get; set; }
        public virtual DbSet<UsersRelation> UserRelations { get; set; }
        public virtual DbSet<SecretMessages> SecretMessage { get; set; }
        public virtual DbSet<SecretChat> SecretChat { get; set; }
        public virtual DbSet<SecretChatGroup> SecretChatGroups { get; set; }
        public virtual DbSet<VideoConference> VideoConferences { get; set; }
        public virtual DbSet<VideoConferenceGroups> VideoConferenceGroups { get; set; }

        public virtual DbSet<UserConnections> UserConnections { get; set; }

        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }

        public virtual DbSet<WorkPlaces> WorkPlaces { get; set; }
        public virtual DbSet<ImportantEvent> ImportantEvents { get; set; }
        public virtual DbSet<Education> Educations { get; set; }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Foto> Fotos { get; set; }
        public virtual DbSet<FotoComment> FotoComments { get; set; }

        public virtual DbSet<ResetPassword> ResetPasswords { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Notification> Notivications { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }

        public virtual DbSet<DisableDialog> DisableDialogs { get; set; }

        public virtual DbSet<BlockedUsersEntries> BlackList { get; set; }

        public virtual DbSet<VkOAuthToken> VkOAuthTokens { get; set; }
    }
}
