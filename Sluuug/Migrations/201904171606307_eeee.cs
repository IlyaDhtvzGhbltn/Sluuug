namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eeee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoConferences", "ConferenceCreatorUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoConferences", "ConferenceCreatorUserId");
        }
    }
}
