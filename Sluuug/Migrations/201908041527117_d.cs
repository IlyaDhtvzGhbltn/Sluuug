namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportantEvents", "TextEventDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImportantEvents", "TextEventDescription", c => c.String(nullable: false));
        }
    }
}
