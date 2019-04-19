namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answeruserid2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VideoConferences", "Offer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VideoConferences", "Offer", c => c.String(nullable: false));
        }
    }
}
