namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendsRelationships", "UserOferFrienshipSender", c => c.Long(nullable: false));
            DropColumn("dbo.FriendsRelationships", "UserSender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendsRelationships", "UserSender", c => c.Long(nullable: false));
            DropColumn("dbo.FriendsRelationships", "UserOferFrienshipSender");
        }
    }
}
