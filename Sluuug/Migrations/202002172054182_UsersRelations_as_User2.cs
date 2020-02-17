namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersRelations_as_User2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersRelations", "UserConfirmer_Id", "dbo.Users");
            DropForeignKey("dbo.UsersRelations", "UserOferFrienshipSender_Id", "dbo.Users");
            DropIndex("dbo.UsersRelations", new[] { "UserConfirmer_Id" });
            DropIndex("dbo.UsersRelations", new[] { "UserOferFrienshipSender_Id" });
            AlterColumn("dbo.UsersRelations", "UserConfirmer_Id", c => c.Int());
            AlterColumn("dbo.UsersRelations", "UserOferFrienshipSender_Id", c => c.Int());
            CreateIndex("dbo.UsersRelations", "UserConfirmer_Id");
            CreateIndex("dbo.UsersRelations", "UserOferFrienshipSender_Id");
            AddForeignKey("dbo.UsersRelations", "UserConfirmer_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UsersRelations", "UserOferFrienshipSender_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRelations", "UserOferFrienshipSender_Id", "dbo.Users");
            DropForeignKey("dbo.UsersRelations", "UserConfirmer_Id", "dbo.Users");
            DropIndex("dbo.UsersRelations", new[] { "UserOferFrienshipSender_Id" });
            DropIndex("dbo.UsersRelations", new[] { "UserConfirmer_Id" });
            AlterColumn("dbo.UsersRelations", "UserOferFrienshipSender_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.UsersRelations", "UserConfirmer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UsersRelations", "UserOferFrienshipSender_Id");
            CreateIndex("dbo.UsersRelations", "UserConfirmer_Id");
            AddForeignKey("dbo.UsersRelations", "UserOferFrienshipSender_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersRelations", "UserConfirmer_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
