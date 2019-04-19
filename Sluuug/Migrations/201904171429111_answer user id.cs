namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answeruserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoConferences", "OfferSenderUser", c => c.Int(nullable: false));
            AddColumn("dbo.VideoConferences", "AnswerSenderUser", c => c.Int(nullable: false));
            DropColumn("dbo.VideoConferences", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VideoConferences", "CreatorUserId", c => c.Int(nullable: false));
            DropColumn("dbo.VideoConferences", "AnswerSenderUser");
            DropColumn("dbo.VideoConferences", "OfferSenderUser");
        }
    }
}
