namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventComment_non_requered : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemorableEvents", "EventComment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemorableEvents", "EventComment", c => c.String(nullable: false));
        }
    }
}
