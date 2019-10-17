namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserType");
        }
    }
}
