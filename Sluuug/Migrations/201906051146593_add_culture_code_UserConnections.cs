namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_culture_code_UserConnections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserConnections", "CultureCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserConnections", "CultureCode");
        }
    }
}
