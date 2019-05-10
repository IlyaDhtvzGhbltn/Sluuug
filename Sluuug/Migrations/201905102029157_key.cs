namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class key : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "User_Id", c => c.Int());
            AddColumn("dbo.MemorableEvents", "User_Id", c => c.Int());
            AddColumn("dbo.LifePlaces", "User_Id", c => c.Int());
            AddColumn("dbo.WorkPlaces", "User_Id", c => c.Int());
            CreateIndex("dbo.Educations", "User_Id");
            CreateIndex("dbo.MemorableEvents", "User_Id");
            CreateIndex("dbo.LifePlaces", "User_Id");
            CreateIndex("dbo.WorkPlaces", "User_Id");
            AddForeignKey("dbo.MemorableEvents", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.LifePlaces", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.WorkPlaces", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Educations", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Educations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WorkPlaces", "User_Id", "dbo.Users");
            DropForeignKey("dbo.LifePlaces", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MemorableEvents", "User_Id", "dbo.Users");
            DropIndex("dbo.WorkPlaces", new[] { "User_Id" });
            DropIndex("dbo.LifePlaces", new[] { "User_Id" });
            DropIndex("dbo.MemorableEvents", new[] { "User_Id" });
            DropIndex("dbo.Educations", new[] { "User_Id" });
            DropColumn("dbo.WorkPlaces", "User_Id");
            DropColumn("dbo.LifePlaces", "User_Id");
            DropColumn("dbo.MemorableEvents", "User_Id");
            DropColumn("dbo.Educations", "User_Id");
        }
    }
}
