namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imgtbo_add_publicID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "ImagePublicID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fotoes", "ImagePublicID");
        }
    }
}
