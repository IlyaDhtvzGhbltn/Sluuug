namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answernull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VideoConferences", "Answer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VideoConferences", "Answer", c => c.String(nullable: false));
        }
    }
}
