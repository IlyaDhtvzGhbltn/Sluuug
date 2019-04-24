namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecretMessagetbononuniquekeyPartyId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            CreateIndex("dbo.SecretMessages", "PartyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SecretMessages", new[] { "PartyId" });
            CreateIndex("dbo.SecretMessages", "PartyId", unique: true);
        }
    }
}
