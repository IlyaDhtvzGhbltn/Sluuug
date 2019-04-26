namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friendship_tbo_enum_ststus_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendsRelationships", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.FriendsRelationships", "IsAccepted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendsRelationships", "IsAccepted", c => c.Boolean(nullable: false));
            DropColumn("dbo.FriendsRelationships", "Status");
        }
    }
}
