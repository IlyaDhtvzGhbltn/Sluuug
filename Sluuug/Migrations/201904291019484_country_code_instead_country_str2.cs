namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class country_code_instead_country_str2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "SityCode", c => c.String(nullable: false));
            AddColumn("dbo.LifePlaces", "SityCode", c => c.String(nullable: false));
            AddColumn("dbo.WorkPlaces", "SityCode", c => c.String(nullable: false));
            DropColumn("dbo.Educations", "Sity");
            DropColumn("dbo.LifePlaces", "Sity");
            DropColumn("dbo.WorkPlaces", "Sity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkPlaces", "Sity", c => c.String(nullable: false));
            AddColumn("dbo.LifePlaces", "Sity", c => c.String(nullable: false));
            AddColumn("dbo.Educations", "Sity", c => c.String(nullable: false));
            DropColumn("dbo.WorkPlaces", "SityCode");
            DropColumn("dbo.LifePlaces", "SityCode");
            DropColumn("dbo.Educations", "SityCode");
        }
    }
}
