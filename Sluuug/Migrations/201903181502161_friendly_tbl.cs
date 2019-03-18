namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendly_tbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendsRelationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferSended = c.DateTime(nullable: false),
                        UserSender = c.Long(nullable: false),
                        UserConfirmer = c.Long(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FriendsRelationships");
        }
    }
}
