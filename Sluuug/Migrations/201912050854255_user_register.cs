namespace Slug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_register : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RegisterDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RegisterDate");
        }
    }
}
