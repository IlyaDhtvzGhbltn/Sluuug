namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imges_tbls2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarId", c => c.Int());
            RenameTable("dbo.UserStatus", "UserStatuses");
        }
        
        public override void Down()
        {
        }
    }
}
