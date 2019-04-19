namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class max_length_36 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.String(nullable: false, maxLength: 36));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SecretMessages", "PartyId", c => c.String(nullable: false));
        }
    }
}
