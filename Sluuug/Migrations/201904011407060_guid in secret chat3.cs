namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guidinsecretchat3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecretChats", "PartyGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SecretChats", "PartyGuid");
        }
    }
}
