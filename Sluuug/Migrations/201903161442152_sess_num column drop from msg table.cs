namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sess_numcolumndropfrommsgtable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "SessionNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "SessionNumber", c => c.String(nullable: false, maxLength: 120));
        }
    }
}
