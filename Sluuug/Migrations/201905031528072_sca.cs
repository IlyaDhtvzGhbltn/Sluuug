namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sca : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SecretChatGroups", "Accepted2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SecretChatGroups", "Accepted2", c => c.Boolean(nullable: false));
        }
    }
}
