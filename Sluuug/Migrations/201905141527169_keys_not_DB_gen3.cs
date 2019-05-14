namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys_not_DB_gen3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums");
            DropIndex("dbo.Fotoes", new[] { "AlbumID" });
            DropTable("dbo.Albums");
        }
        
        public override void Down()
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Fotoes", "AlbumID");
            AddForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}
