namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserinfos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "NowCountry", c => c.String());
            AddColumn("dbo.UserInfoes", "NowSity", c => c.String());
            AddColumn("dbo.UserInfoes", "NowMetroStation", c => c.String());
            DropColumn("dbo.UserInfoes", "Country");
            DropColumn("dbo.UserInfoes", "Sity");
            DropColumn("dbo.UserInfoes", "MetroStation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "MetroStation", c => c.String());
            AddColumn("dbo.UserInfoes", "Sity", c => c.String());
            AddColumn("dbo.UserInfoes", "Country", c => c.String());
            DropColumn("dbo.UserInfoes", "NowMetroStation");
            DropColumn("dbo.UserInfoes", "NowSity");
            DropColumn("dbo.UserInfoes", "NowCountry");
        }
    }
}
