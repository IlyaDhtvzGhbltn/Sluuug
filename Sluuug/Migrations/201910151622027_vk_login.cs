namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vk_login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "VkUserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "VkUserId");
        }
    }
}
