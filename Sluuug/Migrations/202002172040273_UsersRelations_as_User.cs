namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersRelations_as_User : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.UsersRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferSendedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        IsInvitationSeen = c.Boolean(nullable: false),
                        BlockEntrie = c.Guid(nullable: false),
                        UserConfirmer_Id = c.Int(nullable: false),
                        UserOferFrienshipSender_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserConfirmer_Id, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserOferFrienshipSender_Id, cascadeDelete: false)
                .Index(t => t.UserConfirmer_Id)
                .Index(t => t.UserOferFrienshipSender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRelations", "UserOferFrienshipSender_Id", "dbo.Users");
            DropForeignKey("dbo.UsersRelations", "UserConfirmer_Id", "dbo.Users");
            DropIndex("dbo.UsersRelations", new[] { "UserOferFrienshipSender_Id" });
            DropIndex("dbo.UsersRelations", new[] { "UserConfirmer_Id" });
            DropTable("dbo.UsersRelations");
        }
    }
}
