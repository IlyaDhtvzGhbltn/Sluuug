namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid_for_entrys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.MemorableEvents", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.LifePlaces", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.WorkPlaces", "EntryId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkPlaces", "EntryId");
            DropColumn("dbo.LifePlaces", "EntryId");
            DropColumn("dbo.MemorableEvents", "EntryId");
            DropColumn("dbo.Educations", "EntryId");
        }
    }
}
