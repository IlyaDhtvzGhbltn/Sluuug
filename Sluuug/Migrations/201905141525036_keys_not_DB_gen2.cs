namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys_not_DB_gen2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "User_Id", "dbo.Users");
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropColumn("dbo.Albums", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "User_Id", c => c.Int());
            CreateIndex("dbo.Albums", "User_Id");
            AddForeignKey("dbo.Albums", "User_Id", "dbo.Users", "Id");
        }
    }
}
