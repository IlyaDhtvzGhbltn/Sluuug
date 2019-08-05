namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class album_w_h : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "LabelOriginalHeight", c => c.Long(nullable: false));
            AddColumn("dbo.Albums", "LabelOriginalWidth", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "LabelOriginalWidth");
            DropColumn("dbo.Albums", "LabelOriginalHeight");
        }
    }
}
