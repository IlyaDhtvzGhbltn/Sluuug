namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phototbo_heightwidth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "Height", c => c.Long(nullable: false));
            AddColumn("dbo.Fotoes", "Width", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fotoes", "Width");
            DropColumn("dbo.Fotoes", "Height");
        }
    }
}
