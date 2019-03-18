namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name_fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendsRelationships", "OfferSendedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.FriendsRelationships", "OfferSended");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendsRelationships", "OfferSended", c => c.DateTime(nullable: false));
            DropColumn("dbo.FriendsRelationships", "OfferSendedDate");
        }
    }
}
