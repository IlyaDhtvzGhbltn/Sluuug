﻿using System;
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

        public DbSet<ActivationLink> ActivationLink { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionType> SessionTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messangers { get; set; }
        public DbSet<Avatars> Avatars { get; set; }
    }
}
