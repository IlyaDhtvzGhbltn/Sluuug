namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_unmerg_to_settings_and_info : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotifyWhenLogin = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        PasswordHash = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Settings_Id", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "DateOfBirth", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Users", "Settings_Id");
            AddForeignKey("dbo.Users", "Settings_Id", "dbo.UserSettings", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "DateOfBirth");
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropForeignKey("dbo.Users", "Settings_Id", "dbo.UserSettings");
            DropIndex("dbo.Users", new[] { "Settings_Id" });
            DropColumn("dbo.UserInfoes", "DateOfBirth");
            DropColumn("dbo.Users", "Settings_Id");
            DropTable("dbo.UserSettings");
        }
    }
}
