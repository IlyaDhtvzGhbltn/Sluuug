namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys_not_DB_gen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums");
            DropPrimaryKey("dbo.Albums");
            AlterColumn("dbo.Albums", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Albums", "Id");
            AddForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums");
            DropPrimaryKey("dbo.Albums");
            AlterColumn("dbo.Albums", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Albums", "Id");
            AddForeignKey("dbo.Fotoes", "AlbumID", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}
