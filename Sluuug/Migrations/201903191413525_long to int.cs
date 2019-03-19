namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class longtoint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FriendsRelationships", "UserOferFrienshipSender", c => c.Int(nullable: false));
            AlterColumn("dbo.FriendsRelationships", "UserConfirmer", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FriendsRelationships", "UserConfirmer", c => c.Long(nullable: false));
            AlterColumn("dbo.FriendsRelationships", "UserOferFrienshipSender", c => c.Long(nullable: false));
        }
    }
}
