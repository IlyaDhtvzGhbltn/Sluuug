namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secrettbo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecretChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(nullable: false),
                        Create = c.DateTime(nullable: false),
                        Destroy = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecretChatGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecretMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(nullable: false),
                        UserSender = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        SendingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SecretMessages");
            DropTable("dbo.SecretChatGroups");
            DropTable("dbo.SecretChats");
        }
    }
}
