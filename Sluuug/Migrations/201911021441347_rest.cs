namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rest : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "IdVkUser", c => c.Long(nullable: false));
            AddColumn("dbo.UserInfoes", "IdFBUser", c => c.Long(nullable: false));
            AddColumn("dbo.Avatars", "LargeAvatar", c => c.String(nullable: false));
            AddColumn("dbo.Avatars", "MediumAvatar", c => c.String());
            AddColumn("dbo.Avatars", "SmallAvatar", c => c.String());
            AddColumn("dbo.Avatars", "AvatarType", c => c.Int(nullable: false));
            AddColumn("dbo.UserConnections", "UpdateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Avatars", "ImgPath");
            DropColumn("dbo.UserConnections", "OpenTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserConnections", "OpenTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Avatars", "ImgPath", c => c.String(nullable: false));
            DropColumn("dbo.UserConnections", "UpdateTime");
            DropColumn("dbo.Avatars", "AvatarType");
            DropColumn("dbo.Avatars", "SmallAvatar");
            DropColumn("dbo.Avatars", "MediumAvatar");
            DropColumn("dbo.Avatars", "LargeAvatar");
            DropColumn("dbo.UserInfoes", "IdFBUser");
            DropColumn("dbo.UserInfoes", "IdVkUser");
            DropColumn("dbo.Users", "UserType");
            DropTable("dbo.VkOAuthTokens");
        }
    }
}
