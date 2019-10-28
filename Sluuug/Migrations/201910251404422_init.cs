namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivationLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivationParam = c.String(maxLength: 120),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        CreateUserID = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        AlbumLabelUrl = c.String(nullable: false),
                        LabelOriginalHeight = c.Long(nullable: false),
                        LabelOriginalWidth = c.Long(nullable: false),
                        AlbumLabesPublicID = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FotoGUID = c.Guid(nullable: false),
                        AlbumID = c.Guid(nullable: false),
                        Title = c.String(),
                        Url = c.String(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        UploadUserID = c.Int(nullable: false),
                        ImagePublicID = c.String(nullable: false),
                        Height = c.Long(nullable: false),
                        Width = c.Long(nullable: false),
                        PositiveRating = c.Int(nullable: false),
                        NegativeRating = c.Int(nullable: false),
                        Description = c.String(),
                        ImportantEvent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumID, cascadeDelete: true)
                .ForeignKey("dbo.ImportantEvents", t => t.ImportantEvent_Id)
                .Index(t => t.AlbumID)
                .Index(t => t.ImportantEvent_Id);
            
            CreateTable(
                "dbo.FotoComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserCommenter = c.Int(nullable: false),
                        CommentText = c.String(nullable: false),
                        CommentWriteDate = c.DateTime(nullable: false),
                        Foto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fotoes", t => t.Foto_Id)
                .Index(t => t.Foto_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        UserStatus = c.Int(nullable: false),
                        AvatarId = c.Int(),
                        UserType = c.Int(nullable: false),
                        Settings_Id = c.Int(),
                        UserFullInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserSettings", t => t.Settings_Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserFullInfo_Id)
                .Index(t => t.Settings_Id)
                .Index(t => t.UserFullInfo_Id);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationType = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        PasswordHash = c.String(nullable: false),
                        QuickMessage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfBirth = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        SurName = c.String(nullable: false, maxLength: 20),
                        Sex = c.Int(nullable: false),
                        NowCountryCode = c.Int(nullable: false),
                        NowCityCode = c.Int(nullable: false),
                        HelloMessage = c.String(),
                        userDatingSex = c.Int(nullable: false),
                        DatingPurpose = c.Int(nullable: false),
                        userDatingAge = c.Int(nullable: false),
                        IdVkUser = c.Long(nullable: false),
                        IdFBUser = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EducationType = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Faculty = c.String(),
                        Specialty = c.String(),
                        CountryCode = c.Int(nullable: false),
                        CityCode = c.Int(nullable: false),
                        UntilNow = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Comment = c.String(maxLength: 500),
                        PersonalRating = c.Int(nullable: false),
                        User_Id = c.Int(),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.ImportantEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AlbumGuid = c.Guid(nullable: false),
                        EventTitle = c.String(nullable: false),
                        DateEvent = c.DateTime(nullable: false),
                        TextEventDescription = c.String(),
                        User_Id = c.Int(nullable: false),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.WorkPlaces",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CompanyTitle = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        CountryCode = c.Int(nullable: false),
                        CityCode = c.Int(nullable: false),
                        UntilNow = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Comment = c.String(maxLength: 500),
                        PersonalRating = c.Int(nullable: false),
                        User_Id = c.Int(),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UploadTime = c.DateTime(nullable: false),
                        IsStandart = c.Boolean(nullable: false),
                        CountryCode = c.Int(),
                        LargeAvatar = c.String(nullable: false),
                        MediumAvatar = c.String(),
                        SmallAvatar = c.String(),
                        AvatarType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlockedUsersEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlockEntryId = c.Guid(nullable: false),
                        UserBlocker = c.Int(nullable: false),
                        UserBlocked = c.Int(nullable: false),
                        BlockDate = c.DateTime(nullable: false),
                        HateMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CitiesCode = c.Int(nullable: false),
                        CountryCode = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConversationGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationGuidId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationGuidId = c.Guid(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ConversationGuidId, unique: true);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DisableDialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationId = c.Guid(nullable: false),
                        UserDisablerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 1000),
                        BackEmeil = c.String(),
                        UserID = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConvarsationGuidId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        SendingDate = c.DateTime(nullable: false),
                        IsReaded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        UserRecipient = c.Int(nullable: false),
                        UserSender = c.Int(nullable: false),
                        Message = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserCommenter = c.Int(nullable: false),
                        CommentText = c.String(nullable: false),
                        CommentWriteDate = c.DateTime(nullable: false),
                        Post_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PublicDateTime = c.DateTime(nullable: false),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        UserPosted_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserPosted_Id, cascadeDelete: true)
                .Index(t => t.UserPosted_Id);
            
            CreateTable(
                "dbo.ResetPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserRequestedId = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        ResetParameter = c.String(nullable: false, maxLength: 120),
                        CreateRequestDate = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecretChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyGUID = c.Guid(nullable: false),
                        Create = c.DateTime(nullable: false),
                        Destroy = c.DateTime(nullable: false),
                        CreatorUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecretChatGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyGUID = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecretMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyId = c.Guid(nullable: false),
                        UserSender = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        SendingDate = c.DateTime(nullable: false),
                        IsReaded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.PartyId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 120),
                        Type = c.Int(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        Expired = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SessionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionId = c.Guid(nullable: false),
                        IpAddress = c.String(nullable: false),
                        CultureCode = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        ConnectionOff = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferSendedDate = c.DateTime(nullable: false),
                        UserOferFrienshipSender = c.Int(nullable: false),
                        UserConfirmer = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsInvitationSeen = c.Boolean(nullable: false),
                        BlockEntrie = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoConferenceGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoConferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Offer = c.String(),
                        Answer = c.String(),
                        ConferenceCreatorUserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.GuidId, unique: true);
            
            CreateTable(
                "dbo.VkOAuthTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 36),
                        Token = c.String(nullable: false, maxLength: 85),
                        VkUserId = c.Long(nullable: false),
                        ReceivedDate = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users");
            DropForeignKey("dbo.PostComments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Albums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "UserFullInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.WorkPlaces", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.WorkPlaces", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ImportantEvents", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.ImportantEvents", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Fotoes", "ImportantEvent_Id", "dbo.ImportantEvents");
            DropForeignKey("dbo.Educations", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.Educations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Settings_Id", "dbo.UserSettings");
            DropForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums");
            DropForeignKey("dbo.FotoComments", "Foto_Id", "dbo.Fotoes");
            DropIndex("dbo.VideoConferences", new[] { "GuidId" });
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            DropIndex("dbo.Posts", new[] { "UserPosted_Id" });
            DropIndex("dbo.PostComments", new[] { "Post_Id" });
            DropIndex("dbo.Conversations", new[] { "ConversationGuidId" });
            DropIndex("dbo.WorkPlaces", new[] { "UserInfo_Id" });
            DropIndex("dbo.WorkPlaces", new[] { "User_Id" });
            DropIndex("dbo.ImportantEvents", new[] { "UserInfo_Id" });
            DropIndex("dbo.ImportantEvents", new[] { "User_Id" });
            DropIndex("dbo.Educations", new[] { "UserInfo_Id" });
            DropIndex("dbo.Educations", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "UserFullInfo_Id" });
            DropIndex("dbo.Users", new[] { "Settings_Id" });
            DropIndex("dbo.FotoComments", new[] { "Foto_Id" });
            DropIndex("dbo.Fotoes", new[] { "ImportantEvent_Id" });
            DropIndex("dbo.Fotoes", new[] { "AlbumID" });
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropTable("dbo.VkOAuthTokens");
            DropTable("dbo.VideoConferences");
            DropTable("dbo.VideoConferenceGroups");
            DropTable("dbo.UserStatus");
            DropTable("dbo.UsersRelations");
            DropTable("dbo.UserConnections");
            DropTable("dbo.SessionTypes");
            DropTable("dbo.Sessions");
            DropTable("dbo.SecretMessages");
            DropTable("dbo.SecretChatGroups");
            DropTable("dbo.SecretChats");
            DropTable("dbo.ResetPasswords");
            DropTable("dbo.Posts");
            DropTable("dbo.PostComments");
            DropTable("dbo.Notifications");
            DropTable("dbo.Messages");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.DisableDialogs");
            DropTable("dbo.Countries");
            DropTable("dbo.Conversations");
            DropTable("dbo.ConversationGroups");
            DropTable("dbo.Cities");
            DropTable("dbo.BlockedUsersEntries");
            DropTable("dbo.Avatars");
            DropTable("dbo.WorkPlaces");
            DropTable("dbo.ImportantEvents");
            DropTable("dbo.Educations");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.UserSettings");
            DropTable("dbo.Users");
            DropTable("dbo.FotoComments");
            DropTable("dbo.Fotoes");
            DropTable("dbo.Albums");
            DropTable("dbo.ActivationLinks");
        }
    }
}
