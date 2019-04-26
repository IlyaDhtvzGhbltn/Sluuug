namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fillcollumnsintoinfotables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Educations", "EducationType", c => c.Int(nullable: false));
            AddColumn("dbo.Educations", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Educations", "Faculty", c => c.String());
            AddColumn("dbo.Educations", "Specialty", c => c.String());
            AddColumn("dbo.Educations", "CountryCode", c => c.Int(nullable: false));
            AddColumn("dbo.Educations", "Sity", c => c.String(nullable: false));
            AddColumn("dbo.Educations", "UntilNow", c => c.Boolean(nullable: false));
            AddColumn("dbo.Educations", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Educations", "End", c => c.DateTime());
            AddColumn("dbo.Educations", "Comment", c => c.String(maxLength: 500));
            AddColumn("dbo.Educations", "PersonalRating", c => c.Int(nullable: false));
            AddColumn("dbo.MemorableEvents", "EventTitle", c => c.String(nullable: false));
            AddColumn("dbo.MemorableEvents", "EventComment", c => c.String(nullable: false));
            AddColumn("dbo.LifePlaces", "CountryCode", c => c.Int(nullable: false));
            AddColumn("dbo.LifePlaces", "Sity", c => c.String(nullable: false));
            AddColumn("dbo.LifePlaces", "UntilNow", c => c.Boolean(nullable: false));
            AddColumn("dbo.LifePlaces", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.LifePlaces", "End", c => c.DateTime());
            AddColumn("dbo.LifePlaces", "Comment", c => c.String(maxLength: 500));
            AddColumn("dbo.LifePlaces", "PersonalRating", c => c.Int(nullable: false));
            AddColumn("dbo.WorkPlaces", "CompanyTitle", c => c.String(nullable: false));
            AddColumn("dbo.WorkPlaces", "Position", c => c.String(nullable: false));
            AddColumn("dbo.WorkPlaces", "CountryCode", c => c.Int(nullable: false));
            AddColumn("dbo.WorkPlaces", "Sity", c => c.String(nullable: false));
            AddColumn("dbo.WorkPlaces", "UntilNow", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkPlaces", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkPlaces", "End", c => c.DateTime());
            AddColumn("dbo.WorkPlaces", "Comment", c => c.String(maxLength: 500));
            AddColumn("dbo.WorkPlaces", "PersonalRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkPlaces", "PersonalRating");
            DropColumn("dbo.WorkPlaces", "Comment");
            DropColumn("dbo.WorkPlaces", "End");
            DropColumn("dbo.WorkPlaces", "Start");
            DropColumn("dbo.WorkPlaces", "UntilNow");
            DropColumn("dbo.WorkPlaces", "Sity");
            DropColumn("dbo.WorkPlaces", "CountryCode");
            DropColumn("dbo.WorkPlaces", "Position");
            DropColumn("dbo.WorkPlaces", "CompanyTitle");
            DropColumn("dbo.LifePlaces", "PersonalRating");
            DropColumn("dbo.LifePlaces", "Comment");
            DropColumn("dbo.LifePlaces", "End");
            DropColumn("dbo.LifePlaces", "Start");
            DropColumn("dbo.LifePlaces", "UntilNow");
            DropColumn("dbo.LifePlaces", "Sity");
            DropColumn("dbo.LifePlaces", "CountryCode");
            DropColumn("dbo.MemorableEvents", "EventComment");
            DropColumn("dbo.MemorableEvents", "EventTitle");
            DropColumn("dbo.Educations", "PersonalRating");
            DropColumn("dbo.Educations", "Comment");
            DropColumn("dbo.Educations", "End");
            DropColumn("dbo.Educations", "Start");
            DropColumn("dbo.Educations", "UntilNow");
            DropColumn("dbo.Educations", "Sity");
            DropColumn("dbo.Educations", "CountryCode");
            DropColumn("dbo.Educations", "Specialty");
            DropColumn("dbo.Educations", "Faculty");
            DropColumn("dbo.Educations", "Title");
            DropColumn("dbo.Educations", "EducationType");
        }
    }
}
