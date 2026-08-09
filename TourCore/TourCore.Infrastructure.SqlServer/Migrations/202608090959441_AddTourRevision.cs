namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourRevision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Revision", c => c.Long(nullable: false, defaultValue: 1L));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "Revision");
        }
    }
}
