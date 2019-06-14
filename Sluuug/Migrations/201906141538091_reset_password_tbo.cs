namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset_password_tbo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResetPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserRequestedId = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        ResetParameter = c.String(nullable: false, maxLength: 120),
                        CreateRequestDate = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResetPasswords");
        }
    }
}
