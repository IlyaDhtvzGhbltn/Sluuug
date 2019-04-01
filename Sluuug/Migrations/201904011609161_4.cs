namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecretChatGroups", "PartyGUID", c => c.Guid(nullable: false));
            DropColumn("dbo.SecretChatGroups", "PartyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecretChatGroups", "PartyId", c => c.Guid(nullable: false));
            DropColumn("dbo.SecretChatGroups", "PartyGUID");
        }
    }
}
