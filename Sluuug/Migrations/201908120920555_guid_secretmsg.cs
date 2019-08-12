namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid_secretmsg : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SecretMessages", "PartyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.String(nullable: false, maxLength: 36));
            CreateIndex("dbo.SecretMessages", "PartyId");
        }
    }
}
