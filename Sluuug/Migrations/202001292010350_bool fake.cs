namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolfake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsFakeBot", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsFakeBot");
        }
    }
}
