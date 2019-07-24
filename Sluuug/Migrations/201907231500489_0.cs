namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "userDatingSex", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "DatingPurpose", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "userDatingAge", c => c.Int(nullable: false));
            DropColumn("dbo.UserInfoes", "DatingForStatus");
            DropColumn("dbo.UserInfoes", "userSearchSex");
            DropColumn("dbo.UserInfoes", "purpose");
            DropColumn("dbo.UserInfoes", "userSearchAge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "userSearchAge", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "purpose", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "userSearchSex", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "DatingForStatus", c => c.Int(nullable: false));
            DropColumn("dbo.UserInfoes", "userDatingAge");
            DropColumn("dbo.UserInfoes", "DatingPurpose");
            DropColumn("dbo.UserInfoes", "userDatingSex");
        }
    }
}
