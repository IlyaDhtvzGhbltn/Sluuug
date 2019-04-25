namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendsRelations_Accepted_rename_to_IsAccepted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendsRelationships", "IsAccepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.FriendsRelationships", "Accepted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendsRelationships", "Accepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.FriendsRelationships", "IsAccepted");
        }
    }
}
