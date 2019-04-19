namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class video_chat_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoConferences", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoConferences", "IsActive");
        }
    }
}
