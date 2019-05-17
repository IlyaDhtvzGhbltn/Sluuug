namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pubID_in_Album_label : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Description", c => c.String());
            AddColumn("dbo.Albums", "AlbumLabesPublicID", c => c.String());
            DropColumn("dbo.Albums", "AuthorComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "AuthorComment", c => c.String());
            DropColumn("dbo.Albums", "AlbumLabesPublicID");
            DropColumn("dbo.Albums", "Description");
        }
    }
}
