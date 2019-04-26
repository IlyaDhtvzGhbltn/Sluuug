namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userinformationtbo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        Sity = c.String(),
                        MetroStation = c.String(),
                        PrivateStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.MemorableEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.LifePlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.WorkPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            AddColumn("dbo.Users", "UserFullInfo_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserFullInfo_Id");
            AddForeignKey("dbo.Users", "UserFullInfo_Id", "dbo.UserInfoes", "Id");
            DropColumn("dbo.Users", "Sity");
            DropColumn("dbo.Users", "MetroStation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "MetroStation", c => c.String());
            AddColumn("dbo.Users", "Sity", c => c.String());
            DropForeignKey("dbo.Users", "UserFullInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.WorkPlaces", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.LifePlaces", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.MemorableEvents", "UserInfo_Id", "dbo.UserInfoes");
            DropForeignKey("dbo.Educations", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.WorkPlaces", new[] { "UserInfo_Id" });
            DropIndex("dbo.LifePlaces", new[] { "UserInfo_Id" });
            DropIndex("dbo.MemorableEvents", new[] { "UserInfo_Id" });
            DropIndex("dbo.Educations", new[] { "UserInfo_Id" });
            DropIndex("dbo.Users", new[] { "UserFullInfo_Id" });
            DropColumn("dbo.Users", "UserFullInfo_Id");
            DropTable("dbo.WorkPlaces");
            DropTable("dbo.LifePlaces");
            DropTable("dbo.MemorableEvents");
            DropTable("dbo.Educations");
            DropTable("dbo.UserInfoes");
        }
    }
}
