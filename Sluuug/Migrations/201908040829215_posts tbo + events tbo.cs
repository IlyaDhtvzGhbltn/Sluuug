namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poststboeventstbo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Events", newName: "ImportantEvents");
            DropForeignKey("dbo.Events", "User_Id", "dbo.Users");
            DropIndex("dbo.ImportantEvents", new[] { "User_Id" });
            RenameColumn(table: "dbo.Fotoes", name: "Events_Id", newName: "ImportantEvent_Id");
            RenameIndex(table: "dbo.Fotoes", name: "IX_Events_Id", newName: "IX_ImportantEvent_Id");
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserCommenter = c.Int(nullable: false),
                        CommentText = c.String(nullable: false),
                        CommentWriteDate = c.DateTime(nullable: false),
                        Post_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PublicDateTime = c.DateTime(nullable: false),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ImportantEvents", "TextEventDescription", c => c.String(nullable: false));
            AlterColumn("dbo.ImportantEvents", "DateEvent", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ImportantEvents", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ImportantEvents", "User_Id");
            AddForeignKey("dbo.ImportantEvents", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.ImportantEvents", "EventComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImportantEvents", "EventComment", c => c.String());
            DropForeignKey("dbo.ImportantEvents", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PostComments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.PostComments", new[] { "Post_Id" });
            DropIndex("dbo.ImportantEvents", new[] { "User_Id" });
            AlterColumn("dbo.ImportantEvents", "User_Id", c => c.Int());
            AlterColumn("dbo.ImportantEvents", "DateEvent", c => c.DateTime());
            DropColumn("dbo.ImportantEvents", "TextEventDescription");
            DropTable("dbo.Posts");
            DropTable("dbo.PostComments");
            RenameIndex(table: "dbo.Fotoes", name: "IX_ImportantEvent_Id", newName: "IX_Events_Id");
            RenameColumn(table: "dbo.Fotoes", name: "ImportantEvent_Id", newName: "Events_Id");
            CreateIndex("dbo.ImportantEvents", "User_Id");
            AddForeignKey("dbo.Events", "User_Id", "dbo.Users", "Id");
            RenameTable(name: "dbo.ImportantEvents", newName: "Events");
        }
    }
}
