namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "IdOkUser", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "IdOkUser");
        }
    }
}
