namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys_not_DB_gen4 : DbMigration
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
                        AuthorComment = c.String(),
                        AlbumLabelUrl = c.String(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateIndex("dbo.Fotoes", "AlbumID");
            AddForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums");
            DropIndex("dbo.Fotoes", new[] { "AlbumID" });
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropTable("dbo.Albums");
        }
    }
}
