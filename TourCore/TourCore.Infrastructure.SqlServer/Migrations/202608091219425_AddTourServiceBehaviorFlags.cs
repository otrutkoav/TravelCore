namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourServiceBehaviorFlags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourServices", "IsHidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourServices", "IsRemovable", c => c.Boolean(nullable: false));
            AddColumn("dbo.TourServices", "IsOptional", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourServices", "IsOptional");
            DropColumn("dbo.TourServices", "IsRemovable");
            DropColumn("dbo.TourServices", "IsHidden");
        }
    }
}
