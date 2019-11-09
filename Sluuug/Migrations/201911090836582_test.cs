namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "IdVkUser", c => c.Long(nullable: false));
            AddColumn("dbo.UserInfoes", "IdFBUser", c => c.Long(nullable: false));
            DropColumn("dbo.UserInfoes", "VkUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "VkUserId", c => c.Long(nullable: false));
            DropColumn("dbo.UserInfoes", "IdFBUser");
            DropColumn("dbo.UserInfoes", "IdVkUser");
        }
    }
}
