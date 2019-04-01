namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guidinsecretchat4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SecretChats", "PartyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecretChats", "PartyId", c => c.Int(nullable: false));
        }
    }
}
