namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lms_avatarsfromvk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avatars", "LargeAvatar", c => c.String(nullable: false));
            AddColumn("dbo.Avatars", "MediumAvatar", c => c.String());
            AddColumn("dbo.Avatars", "SmallAvatar", c => c.String());
            AddColumn("dbo.Avatars", "AvatarType", c => c.Int(nullable: false));
            DropColumn("dbo.Avatars", "ImgPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avatars", "ImgPath", c => c.String(nullable: false));
            DropColumn("dbo.Avatars", "AvatarType");
            DropColumn("dbo.Avatars", "SmallAvatar");
            DropColumn("dbo.Avatars", "MediumAvatar");
            DropColumn("dbo.Avatars", "LargeAvatar");
        }
    }
}
