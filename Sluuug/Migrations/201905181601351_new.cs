namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        CreateUserID = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        AlbumLabelUrl = c.String(nullable: false),
                        AlbumLabesPublicID = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FotoGUID = c.Guid(nullable: false),
                        AlbumID = c.Guid(nullable: false),
                        Title = c.String(),
                        Url = c.String(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        UploadUserID = c.Int(nullable: false),
                        ImagePublicID = c.String(nullable: false),
                        PositiveRating = c.Int(nullable: false),
                        NegativeRating = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumID, cascadeDelete: true)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "dbo.FotoComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserCommenter = c.Int(nullable: false),
                        CommentText = c.String(nullable: false),
                        CommentWriteDate = c.DateTime(nullable: false),
                        Foto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fotoes", t => t.Foto_Id)
                .Index(t => t.Foto_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums");
            DropForeignKey("dbo.FotoComments", "Foto_Id", "dbo.Fotoes");
            DropIndex("dbo.FotoComments", new[] { "Foto_Id" });
            DropIndex("dbo.Fotoes", new[] { "AlbumID" });
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropTable("dbo.FotoComments");
            DropTable("dbo.Fotoes");
            DropTable("dbo.Albums");
        }
    }
}
