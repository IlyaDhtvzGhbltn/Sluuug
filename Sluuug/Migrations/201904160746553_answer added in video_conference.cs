namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answeraddedinvideo_conference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoConferences", "Answer", c => c.String(nullable: false));
            AlterColumn("dbo.VideoConferences", "Offer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VideoConferences", "Offer", c => c.String(nullable: false, maxLength: 2000));
            DropColumn("dbo.VideoConferences", "Answer");
        }
    }
}
