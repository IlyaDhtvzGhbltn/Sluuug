namespace Sluuug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int_to_guid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ConvarsationGuidId", c => c.Guid(nullable: false));
            DropColumn("dbo.Messages", "ConvarsationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ConvarsationId", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "ConvarsationGuidId");
        }
    }
}
