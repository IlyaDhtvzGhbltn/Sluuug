namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MemorableEvents", newName: "Events");
            DropForeignKey("dbo.LifePlaces", "User_Id", "dbo.Users");
            DropForeignKey("dbo.LifePlaces", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.LifePlaces", new[] { "User_Id" });
            DropIndex("dbo.LifePlaces", new[] { "UserInfo_Id" });
            AddColumn("dbo.Fotoes", "Events_Id", c => c.Guid());
            CreateIndex("dbo.Fotoes", "Events_Id");
            AddForeignKey("dbo.Fotoes", "Events_Id", "dbo.Events", "Id");
            DropTable("dbo.LifePlaces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LifePlaces",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CountryCode = c.Int(nullable: false),
                        CityCode = c.Int(nullable: false),
                        UntilNow = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        Comment = c.String(maxLength: 500),
                        PersonalRating = c.Int(nullable: false),
                        User_Id = c.Int(),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Fotoes", "Events_Id", "dbo.Events");
            DropIndex("dbo.Fotoes", new[] { "Events_Id" });
            DropColumn("dbo.Fotoes", "Events_Id");
            CreateIndex("dbo.LifePlaces", "UserInfo_Id");
            CreateIndex("dbo.LifePlaces", "User_Id");
            AddForeignKey("dbo.LifePlaces", "UserInfo_Id", "dbo.UserInfoes", "Id");
            AddForeignKey("dbo.LifePlaces", "User_Id", "dbo.Users", "Id");
            RenameTable(name: "dbo.Events", newName: "MemorableEvents");
        }
    }
}
