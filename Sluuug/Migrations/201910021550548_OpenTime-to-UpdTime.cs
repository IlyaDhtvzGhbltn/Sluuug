namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpenTimetoUpdTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserConnections", "UpdateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserConnections", "OpenTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserConnections", "OpenTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserConnections", "UpdateTime");
        }
    }
}
