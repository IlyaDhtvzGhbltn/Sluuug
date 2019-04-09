namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecretChatGroups", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SecretChatGroups", "Accepted");
        }
    }
}
