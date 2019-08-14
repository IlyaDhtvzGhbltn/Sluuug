namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class black_list2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BlockedUsersEntries");
            AddColumn("dbo.BlockedUsersEntries", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BlockedUsersEntries", "BlockEntryId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.BlockedUsersEntries", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.BlockedUsersEntries");
            AlterColumn("dbo.BlockedUsersEntries", "BlockEntryId", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.BlockedUsersEntries", "Id");
            AddPrimaryKey("dbo.BlockedUsersEntries", "BlockEntryId");
        }
    }
}
