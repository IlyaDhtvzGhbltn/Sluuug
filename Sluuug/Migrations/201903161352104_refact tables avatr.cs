namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refacttablesavatr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avatars", "UploadTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Avatars", "IsStandart", c => c.Boolean(nullable: false));
            AddColumn("dbo.Avatars", "CountryCode", c => c.Int());
            AddColumn("dbo.Avatars", "ImgPath", c => c.String(nullable: false));
            DropColumn("dbo.Avatars", "StandartId");
            DropColumn("dbo.Avatars", "UserCustomId");
            DropTable("dbo.CustomAvatars");
            DropTable("dbo.StandartAvatars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StandartAvatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.Int(nullable: false),
                        ImgPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomAvatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UploadDate = c.DateTime(nullable: false),
                        ImgPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Avatars", "UserCustomId", c => c.Int());
            AddColumn("dbo.Avatars", "StandartId", c => c.Int());
            DropColumn("dbo.Avatars", "ImgPath");
            DropColumn("dbo.Avatars", "CountryCode");
            DropColumn("dbo.Avatars", "IsStandart");
            DropColumn("dbo.Avatars", "UploadTime");
        }
    }
}
