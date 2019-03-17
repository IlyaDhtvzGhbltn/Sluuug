namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imges_tbls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StandartId = c.Int(),
                        UserCustomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StandartAvatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.Int(nullable: false),
                        ImgPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id); 
        }
        
        public override void Down()
        {
            DropTable("dbo.StandartAvatars");
            DropTable("dbo.Avatars");
        }
    }
}
