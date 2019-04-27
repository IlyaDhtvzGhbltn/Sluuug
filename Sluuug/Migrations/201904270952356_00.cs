namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _00 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            CreateTable(
                "dbo.UserConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionID = c.Guid(nullable: false),
                        UserID = c.Int(nullable: false),
                        ConnectionActiveStatus = c.Boolean(nullable: false),
                        ConnectionTime = c.DateTime(nullable: false),
                        ConnectionOff = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotifyWhenLogin = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        PasswordHash = c.String(nullable: false),
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
                        NowCountry = c.String(),
                        NowSity = c.String(),
                        NowMetroStation = c.String(),
                        PrivateStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EducationType = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Faculty = c.String(),
                        Specialty = c.String(),
                        CountryCode = c.Int(nullable: false),
                        Sity = c.String(nullable: false),
                        UntilNow = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Comment = c.String(maxLength: 500),
                        PersonalRating = c.Int(nullable: false),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.MemorableEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(nullable: false),
                        EventComment = c.String(nullable: false),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.LifePlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.Int(nullable: false),
                        Sity = c.String(nullable: false),
                        UntilNow = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Comment = c.String(maxLength: 500),
                        PersonalRating = c.Int(nullable: false),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.WorkPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyTitle = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        CountryCode = c.Int(nullable: false),
                        Sity = c.String(nullable: false),
                        UntilNow = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Comment = c.String(maxLength: 500),
                        PersonalRating = c.Int(nullable: false),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            AddColumn("dbo.FriendsRelationships", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Settings_Id", c => c.Int());
            AddColumn("dbo.Users", "UserFullInfo_Id", c => c.Int());
            CreateIndex("dbo.SecretMessages", "PartyId");
            CreateIndex("dbo.Users", "Settings_Id");
            CreateIndex("dbo.Users", "UserFullInfo_Id");
            AddForeignKey("dbo.Users", "Settings_Id", "dbo.UserSettings", "Id");
            AddForeignKey("dbo.Users", "UserFullInfo_Id", "dbo.UserInfoes", "Id");
            DropColumn("dbo.FriendsRelationships", "Accepted");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "ForName");
            DropColumn("dbo.Users", "Sity");
            DropColumn("dbo.Users", "MetroStation");
            DropColumn("dbo.Users", "DateOfBirth");
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "MetroStation", c => c.String());
            AddColumn("dbo.Users", "Sity", c => c.String());
            AddColumn("dbo.Users", "ForName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AddColumn("dbo.FriendsRelationships", "Accepted", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Users", "UserFullInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.WorkPlaces", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.LifePlaces", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.MemorableEvents", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.Educations", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.Users", "Settings_Id", "dbo.UserSettings");
            DropIndex("dbo.WorkPlaces", new[] { "UserInfo_Id" });
            DropIndex("dbo.LifePlaces", new[] { "UserInfo_Id" });
            DropIndex("dbo.MemorableEvents", new[] { "UserInfo_Id" });
            DropIndex("dbo.Educations", new[] { "UserInfo_Id" });
            DropIndex("dbo.Users", new[] { "UserFullInfo_Id" });
            DropIndex("dbo.Users", new[] { "Settings_Id" });
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            DropColumn("dbo.Users", "UserFullInfo_Id");
            DropColumn("dbo.Users", "Settings_Id");
            DropColumn("dbo.FriendsRelationships", "Status");
            DropTable("dbo.WorkPlaces");
            DropTable("dbo.LifePlaces");
            DropTable("dbo.MemorableEvents");
            DropTable("dbo.Educations");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.UserSettings");
            DropTable("dbo.UserConnections");
            CreateIndex("dbo.SecretMessages", "PartyId", unique: true);
        }
    }
}
