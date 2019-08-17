namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class res : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            CreateTable(
                "dbo.BlockedUsersEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlockEntryId = c.Guid(nullable: false),
                        UserBlocker = c.Int(nullable: false),
                        UserBlocked = c.Int(nullable: false),
                        BlockDate = c.DateTime(nullable: false),
                        HateMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FriendsRelationships", "BlockEntrie", c => c.Guid(nullable: false));
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SecretMessages", "PartyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.String(nullable: false, maxLength: 36));
            DropColumn("dbo.FriendsRelationships", "BlockEntrie");
            DropTable("dbo.BlockedUsersEntries");
            CreateIndex("dbo.SecretMessages", "PartyId");
        }
    }
}
