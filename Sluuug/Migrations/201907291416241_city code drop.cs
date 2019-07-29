namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class citycodedrop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "CountryCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CountryCode", c => c.Int(nullable: false));
        }
    }
}
