namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ip_address_add_UserConnections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserConnections", "IpAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserConnections", "IpAddress");
        }
    }
}
