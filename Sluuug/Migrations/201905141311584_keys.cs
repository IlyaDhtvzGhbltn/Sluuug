namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "User_Id", c => c.Int());
            CreateIndex("dbo.Albums", "User_Id");
            AddForeignKey("dbo.Albums", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "User_Id", "dbo.Users");
            DropIndex("dbo.Albums", new[] { "User_Id" });
            DropColumn("dbo.Albums", "User_Id");
        }
    }
}
