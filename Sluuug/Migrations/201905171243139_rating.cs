namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "PositiveRating", c => c.Int(nullable: false));
            AddColumn("dbo.Fotoes", "NegativeRating", c => c.Int(nullable: false));
            AddColumn("dbo.Fotoes", "Description", c => c.String());
            AlterColumn("dbo.Fotoes", "ImagePublicID", c => c.String(nullable: false));
            DropColumn("dbo.Fotoes", "AuthorComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fotoes", "AuthorComment", c => c.String());
            AlterColumn("dbo.Fotoes", "ImagePublicID", c => c.String());
            DropColumn("dbo.Fotoes", "Description");
            DropColumn("dbo.Fotoes", "NegativeRating");
            DropColumn("dbo.Fotoes", "PositiveRating");
        }
    }
}
