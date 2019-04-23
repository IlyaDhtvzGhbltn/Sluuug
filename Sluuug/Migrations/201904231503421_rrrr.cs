namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rrrr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserConnections", "ConnectionActiveStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserConnections", "ConnectionOff", c => c.DateTime());
            DropColumn("dbo.UserConnections", "ConnectionStatus");
            DropColumn("dbo.UserConnections", "CloseConnectionTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserConnections", "CloseConnectionTime", c => c.DateTime());
            AddColumn("dbo.UserConnections", "ConnectionStatus", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserConnections", "ConnectionOff");
            DropColumn("dbo.UserConnections", "ConnectionActiveStatus");
        }
    }
}
