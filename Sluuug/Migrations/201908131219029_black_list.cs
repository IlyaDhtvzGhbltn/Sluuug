namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class black_list : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockedUsersEntries",
                c => new
                    {
                        BlockEntryId = c.Guid(nullable: false, identity: true),
                        UserBlocker = c.Int(nullable: false),
                        UserBlocked = c.Int(nullable: false),
                        BlockDate = c.DateTime(nullable: false),
                        HateMessage = c.String(),
                    })
                .PrimaryKey(t => t.BlockEntryId);
            
            AddColumn("dbo.FriendsRelationships", "BlockEntrie", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FriendsRelationships", "BlockEntrie");
            DropTable("dbo.BlockedUsersEntries");
        }
    }
}
