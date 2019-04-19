namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class offeranswerUserIDs : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VideoConferences", "OfferSenderUser");
            DropColumn("dbo.VideoConferences", "AnswerSenderUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VideoConferences", "AnswerSenderUser", c => c.Int(nullable: false));
            AddColumn("dbo.VideoConferences", "OfferSenderUser", c => c.Int(nullable: false));
        }
    }
}
