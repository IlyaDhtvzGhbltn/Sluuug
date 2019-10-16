namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vk_tokens_tbo3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkOAuthTokens", "VkUserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VkOAuthTokens", "VkUserId");
        }
    }
}
