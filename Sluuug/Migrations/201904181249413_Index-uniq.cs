namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Indexuniq : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Conversations", "ConversationGuidId", unique: true);
            CreateIndex("dbo.SecretMessages", "PartyId", unique: true);
            CreateIndex("dbo.VideoConferences", "GuidId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.VideoConferences", new[] { "GuidId" });
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            DropIndex("dbo.Conversations", new[] { "ConversationGuidId" });
        }
    }
}
