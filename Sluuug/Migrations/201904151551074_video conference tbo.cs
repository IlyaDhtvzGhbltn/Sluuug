namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoconferencetbo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoConferenceGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        Creator = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoConferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Offer = c.String(nullable: false, maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideoConferences");
            DropTable("dbo.VideoConferenceGroups");
        }
    }
}
