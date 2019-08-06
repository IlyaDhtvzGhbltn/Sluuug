namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable_int_to_int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "UserId", c => c.Int());
        }
    }
}
