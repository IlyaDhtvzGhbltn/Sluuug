namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inttostr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.Int(nullable: false));
        }
    }
}
