namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.Educations", "User_Id", c => c.Int());
            AddColumn("dbo.MemorableEvents", "DateEvent", c => c.DateTime());
            AddColumn("dbo.MemorableEvents", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.MemorableEvents", "User_Id", c => c.Int());
            AddColumn("dbo.LifePlaces", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.LifePlaces", "User_Id", c => c.Int());
            AddColumn("dbo.WorkPlaces", "EntryId", c => c.Guid(nullable: false));
            AddColumn("dbo.WorkPlaces", "User_Id", c => c.Int());
            AlterColumn("dbo.Educations", "SityCode", c => c.Int(nullable: false));
            AlterColumn("dbo.MemorableEvents", "EventComment", c => c.String());
            AlterColumn("dbo.LifePlaces", "SityCode", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkPlaces", "SityCode", c => c.Int(nullable: false));
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
            AlterColumn("dbo.WorkPlaces", "SityCode", c => c.String(nullable: false));
            AlterColumn("dbo.LifePlaces", "SityCode", c => c.String(nullable: false));
            AlterColumn("dbo.MemorableEvents", "EventComment", c => c.String(nullable: false));
            AlterColumn("dbo.Educations", "SityCode", c => c.String(nullable: false));
            DropColumn("dbo.WorkPlaces", "User_Id");
            DropColumn("dbo.WorkPlaces", "EntryId");
            DropColumn("dbo.LifePlaces", "User_Id");
            DropColumn("dbo.LifePlaces", "EntryId");
            DropColumn("dbo.MemorableEvents", "User_Id");
            DropColumn("dbo.MemorableEvents", "EntryId");
            DropColumn("dbo.MemorableEvents", "DateEvent");
            DropColumn("dbo.Educations", "User_Id");
            DropColumn("dbo.Educations", "EntryId");
        }
    }
}
