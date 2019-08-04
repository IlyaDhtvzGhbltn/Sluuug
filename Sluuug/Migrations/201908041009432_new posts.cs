namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newposts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportantEvents", "AlbumGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.Posts", "UserPosted_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "UserPosted_Id");
            AddForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserPosted_Id" });
            DropColumn("dbo.Posts", "UserPosted_Id");
            DropColumn("dbo.ImportantEvents", "AlbumGuid");
        }
    }
}
