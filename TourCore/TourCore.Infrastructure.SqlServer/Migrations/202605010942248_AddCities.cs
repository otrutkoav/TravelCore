namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        NameEn = c.String(maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 3),
                        SortOrder = c.Int(nullable: false),
                        IsDeparturePoint = c.Boolean(nullable: false),
                        TimeZone = c.String(maxLength: 150),
                        IataCode = c.String(maxLength: 3),
                        Coordinates = c.String(maxLength: 30),
                        RegionId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.CountryId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                        NameEn = c.String(),
                        Code = c.String(),
                        SortOrder = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "RegionId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.Regions");
            DropTable("dbo.Cities");
        }
    }
}
