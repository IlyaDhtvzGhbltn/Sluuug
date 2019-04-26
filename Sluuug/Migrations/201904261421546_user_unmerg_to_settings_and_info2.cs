namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_unmerg_to_settings_and_info2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "Name", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.UserInfoes", "SurName", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Users", "ForName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ForName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.UserInfoes", "SurName");
            DropColumn("dbo.UserInfoes", "Name");
        }
    }
}
