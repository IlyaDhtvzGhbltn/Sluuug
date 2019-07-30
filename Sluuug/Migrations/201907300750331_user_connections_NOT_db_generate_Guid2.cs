namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_connections_NOT_db_generate_Guid2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserConnections");
            AddColumn("dbo.UserConnections", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.UserConnections", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserConnections", "OpenTime", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.UserConnections", "Id");
            DropColumn("dbo.UserConnections", "ConnectionActiveStatus");
            DropColumn("dbo.UserConnections", "ConnectionTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserConnections", "ConnectionTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserConnections", "ConnectionActiveStatus", c => c.Boolean(nullable: false));
            DropPrimaryKey("dbo.UserConnections");
            DropColumn("dbo.UserConnections", "OpenTime");
            DropColumn("dbo.UserConnections", "IsActive");
            DropColumn("dbo.UserConnections", "Id");
            AddPrimaryKey("dbo.UserConnections", "ConnectionID");
        }
    }
}
