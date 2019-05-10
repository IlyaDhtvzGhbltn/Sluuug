namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class city_code_to_int : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemorableEvents", "DateEvent", c => c.DateTime());
            AlterColumn("dbo.Educations", "SityCode", c => c.Int(nullable: false));
            AlterColumn("dbo.LifePlaces", "SityCode", c => c.Int(nullable: false));
            AlterColumn("dbo.WorkPlaces", "SityCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkPlaces", "SityCode", c => c.String(nullable: false));
            AlterColumn("dbo.LifePlaces", "SityCode", c => c.String(nullable: false));
            AlterColumn("dbo.Educations", "SityCode", c => c.String(nullable: false));
            DropColumn("dbo.MemorableEvents", "DateEvent");
        }
    }
}
