namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notrequered : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fotoes", "AuthorComment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fotoes", "AuthorComment", c => c.String(nullable: false));
        }
    }
}
