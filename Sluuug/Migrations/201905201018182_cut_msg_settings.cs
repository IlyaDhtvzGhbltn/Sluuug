namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cut_msg_settings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSettings", "QuickMessage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSettings", "QuickMessage");
        }
    }
}
