namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationsisseen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FriendsRelationships", newName: "UsersRelations");
            AddColumn("dbo.UsersRelations", "IsInvitationSeen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsersRelations", "IsInvitationSeen");
            RenameTable(name: "dbo.UsersRelations", newName: "FriendsRelationships");
        }
    }
}
