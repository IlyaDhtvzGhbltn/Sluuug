namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class country_code_in_city_tbo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "CountryCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cities", "CountryCode");
        }
    }
}
