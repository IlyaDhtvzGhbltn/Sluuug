namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vk_tokens_tbo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VkOAuthTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 36),
                        Token = c.String(nullable: false, maxLength: 85),
                        ReceivedDate = c.DateTime(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VkOAuthTokens");
        }
    }
}
