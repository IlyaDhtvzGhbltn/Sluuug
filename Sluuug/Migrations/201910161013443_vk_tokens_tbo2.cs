namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vk_tokens_tbo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkOAuthTokens", "IsExpired", c => c.Boolean(nullable: false));
            DropColumn("dbo.VkOAuthTokens", "IsUsed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VkOAuthTokens", "IsUsed", c => c.Boolean(nullable: false));
            DropColumn("dbo.VkOAuthTokens", "IsExpired");
        }
    }
}
