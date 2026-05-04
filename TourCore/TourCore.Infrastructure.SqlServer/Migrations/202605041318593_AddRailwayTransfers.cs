namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRailwayTransfers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RailwayTransfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CountryFromId = c.Int(nullable: false),
                        CityFromId = c.Int(nullable: false),
                        CountryToId = c.Int(nullable: false),
                        CityToId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityFromId)
                .ForeignKey("dbo.Cities", t => t.CityToId)
                .ForeignKey("dbo.Countries", t => t.CountryFromId)
                .ForeignKey("dbo.Countries", t => t.CountryToId)
                .Index(t => t.CountryFromId)
                .Index(t => t.CityFromId)
                .Index(t => t.CountryToId)
                .Index(t => t.CityToId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RailwayTransfers", "CountryToId", "dbo.Countries");
            DropForeignKey("dbo.RailwayTransfers", "CountryFromId", "dbo.Countries");
            DropForeignKey("dbo.RailwayTransfers", "CityToId", "dbo.Cities");
            DropForeignKey("dbo.RailwayTransfers", "CityFromId", "dbo.Cities");
            DropIndex("dbo.RailwayTransfers", new[] { "CityToId" });
            DropIndex("dbo.RailwayTransfers", new[] { "CountryToId" });
            DropIndex("dbo.RailwayTransfers", new[] { "CityFromId" });
            DropIndex("dbo.RailwayTransfers", new[] { "CountryFromId" });
            DropTable("dbo.RailwayTransfers");
        }
    }
}
