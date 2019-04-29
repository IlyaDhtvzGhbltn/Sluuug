namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class country_code_instead_country_str : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CitiesCode = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserInfoes", "NowCountryCode", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "NowSityCode", c => c.Int(nullable: false));
            DropColumn("dbo.UserInfoes", "NowCountry");
            DropColumn("dbo.UserInfoes", "NowSity");
            DropColumn("dbo.UserInfoes", "NowMetroStation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "NowMetroStation", c => c.String());
            AddColumn("dbo.UserInfoes", "NowSity", c => c.String());
            AddColumn("dbo.UserInfoes", "NowCountry", c => c.String());
            DropColumn("dbo.UserInfoes", "NowSityCode");
            DropColumn("dbo.UserInfoes", "NowCountryCode");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
