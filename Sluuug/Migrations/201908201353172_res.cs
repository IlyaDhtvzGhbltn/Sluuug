namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class res : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecretMessages", "IsReaded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SecretMessages", "IsReaded");
        }
    }
}