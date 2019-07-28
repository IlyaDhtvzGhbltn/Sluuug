namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datingstatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInfoes", "DatingForStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "DatingForStatus", c => c.Int(nullable: false));
        }
    }
}
