namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "NotificationType", c => c.Int(nullable: false));
            DropColumn("dbo.UserSettings", "NotifyWhenLogin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSettings", "NotifyWhenLogin", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserSettings", "NotificationType");
        }
    }
}
