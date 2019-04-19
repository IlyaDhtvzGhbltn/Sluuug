namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cnvlikecryptoDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationGuidId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Conversations", "ConversationGuidId", c => c.Guid(nullable: false));
            AddColumn("dbo.Conversations", "CreatedDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Conversations", "ConversationId");
            DropColumn("dbo.Conversations", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Conversations", "ConversationId", c => c.Int(nullable: false));
            DropColumn("dbo.Conversations", "CreatedDateTime");
            DropColumn("dbo.Conversations", "ConversationGuidId");
            DropTable("dbo.ConversationGroups");
        }
    }
}
