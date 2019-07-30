namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_connections_NOT_db_generate_Guid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserConnections");
            AlterColumn("dbo.UserConnections", "ConnectionID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.UserConnections", "ConnectionID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserConnections");
            AlterColumn("dbo.UserConnections", "ConnectionID", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserConnections", "ConnectionID");
        }
    }
}
