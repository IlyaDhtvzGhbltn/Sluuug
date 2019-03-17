namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class open_dateconvcolumndrop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Conversations", "OpenDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "OpenDate", c => c.DateTime(nullable: false));
        }
    }
}
