namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            AlterColumn("dbo.Regions", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Regions", "NameEn", c => c.String(maxLength: 100));
            AlterColumn("dbo.Regions", "Code", c => c.String(nullable: false, maxLength: 10));
            AddForeignKey("dbo.Regions", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            AlterColumn("dbo.Regions", "Code", c => c.String());
            AlterColumn("dbo.Regions", "NameEn", c => c.String());
            AlterColumn("dbo.Regions", "Name", c => c.String());
            AddForeignKey("dbo.Regions", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
