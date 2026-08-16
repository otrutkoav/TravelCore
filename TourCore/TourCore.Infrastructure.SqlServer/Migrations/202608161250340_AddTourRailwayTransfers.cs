namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourRailwayTransfers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourRailwayTransferArrivalCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourRailwayTransferId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourRailwayTransfers", t => t.TourRailwayTransferId, cascadeDelete: true)
                .Index(t => new { t.TourRailwayTransferId, t.CityId }, unique: true, name: "UX_TourRailwayTransferArrivalCities_Transfer_City")
                .Index(t => t.CityId, name: "IX_TourRailwayTransferArrivalCities_City");
            
            CreateTable(
                "dbo.TourRailwayTransfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourServiceId = c.Int(nullable: false),
                        DepartureTimeFrom = c.Time(nullable: false, precision: 7),
                        DepartureTimeTo = c.Time(nullable: false, precision: 7),
                        ArrivalTimeFrom = c.Time(nullable: false, precision: 7),
                        ArrivalTimeTo = c.Time(nullable: false, precision: 7),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourServices", t => t.TourServiceId, cascadeDelete: true)
                .Index(t => t.TourServiceId, unique: true, name: "UX_TourRailwayTransfers_TourService");
            
            CreateTable(
                "dbo.TourRailwayTransferArrivalCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourRailwayTransferId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourRailwayTransfers", t => t.TourRailwayTransferId, cascadeDelete: true)
                .Index(t => new { t.TourRailwayTransferId, t.CountryId }, unique: true, name: "UX_TourRailwayTransferArrivalCountries_Transfer_Country")
                .Index(t => t.CountryId, name: "IX_TourRailwayTransferArrivalCountries_Country");
            
            CreateTable(
                "dbo.TourRailwayTransferDepartureCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourRailwayTransferId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourRailwayTransfers", t => t.TourRailwayTransferId, cascadeDelete: true)
                .Index(t => new { t.TourRailwayTransferId, t.CityId }, unique: true, name: "UX_TourRailwayTransferDepartureCities_Transfer_City")
                .Index(t => t.CityId, name: "IX_TourRailwayTransferDepartureCities_City");
            
            CreateTable(
                "dbo.TourRailwayTransferDepartureCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourRailwayTransferId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourRailwayTransfers", t => t.TourRailwayTransferId, cascadeDelete: true)
                .Index(t => new { t.TourRailwayTransferId, t.CountryId }, unique: true, name: "UX_TourRailwayTransferDepartureCountries_Transfer_Country")
                .Index(t => t.CountryId, name: "IX_TourRailwayTransferDepartureCountries_Country");
            
            CreateTable(
                "dbo.TourRailwayTransferOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourRailwayTransferId = c.Int(nullable: false),
                        RailwayTransferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourRailwayTransfers", t => t.TourRailwayTransferId, cascadeDelete: true)
                .Index(t => new { t.TourRailwayTransferId, t.RailwayTransferId }, unique: true, name: "UX_TourRailwayTransferOptions_Transfer_Option")
                .Index(t => t.RailwayTransferId, name: "IX_TourRailwayTransferOptions_RailwayTransfer");
            
            CreateTable(
                "dbo.TourRailwayTransferTransports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourRailwayTransferId = c.Int(nullable: false),
                        TransportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourRailwayTransfers", t => t.TourRailwayTransferId, cascadeDelete: true)
                .Index(t => new { t.TourRailwayTransferId, t.TransportId }, unique: true, name: "UX_TourRailwayTransferTransports_Transfer_Transport")
                .Index(t => t.TransportId, name: "IX_TourRailwayTransferTransports_Transport");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourRailwayTransferArrivalCities", "TourRailwayTransferId", "dbo.TourRailwayTransfers");
            DropForeignKey("dbo.TourRailwayTransferTransports", "TourRailwayTransferId", "dbo.TourRailwayTransfers");
            DropForeignKey("dbo.TourRailwayTransfers", "TourServiceId", "dbo.TourServices");
            DropForeignKey("dbo.TourRailwayTransferOptions", "TourRailwayTransferId", "dbo.TourRailwayTransfers");
            DropForeignKey("dbo.TourRailwayTransferDepartureCountries", "TourRailwayTransferId", "dbo.TourRailwayTransfers");
            DropForeignKey("dbo.TourRailwayTransferDepartureCities", "TourRailwayTransferId", "dbo.TourRailwayTransfers");
            DropForeignKey("dbo.TourRailwayTransferArrivalCountries", "TourRailwayTransferId", "dbo.TourRailwayTransfers");
            DropIndex("dbo.TourRailwayTransferTransports", "IX_TourRailwayTransferTransports_Transport");
            DropIndex("dbo.TourRailwayTransferTransports", "UX_TourRailwayTransferTransports_Transfer_Transport");
            DropIndex("dbo.TourRailwayTransferOptions", "IX_TourRailwayTransferOptions_RailwayTransfer");
            DropIndex("dbo.TourRailwayTransferOptions", "UX_TourRailwayTransferOptions_Transfer_Option");
            DropIndex("dbo.TourRailwayTransferDepartureCountries", "IX_TourRailwayTransferDepartureCountries_Country");
            DropIndex("dbo.TourRailwayTransferDepartureCountries", "UX_TourRailwayTransferDepartureCountries_Transfer_Country");
            DropIndex("dbo.TourRailwayTransferDepartureCities", "IX_TourRailwayTransferDepartureCities_City");
            DropIndex("dbo.TourRailwayTransferDepartureCities", "UX_TourRailwayTransferDepartureCities_Transfer_City");
            DropIndex("dbo.TourRailwayTransferArrivalCountries", "IX_TourRailwayTransferArrivalCountries_Country");
            DropIndex("dbo.TourRailwayTransferArrivalCountries", "UX_TourRailwayTransferArrivalCountries_Transfer_Country");
            DropIndex("dbo.TourRailwayTransfers", "UX_TourRailwayTransfers_TourService");
            DropIndex("dbo.TourRailwayTransferArrivalCities", "IX_TourRailwayTransferArrivalCities_City");
            DropIndex("dbo.TourRailwayTransferArrivalCities", "UX_TourRailwayTransferArrivalCities_Transfer_City");
            DropTable("dbo.TourRailwayTransferTransports");
            DropTable("dbo.TourRailwayTransferOptions");
            DropTable("dbo.TourRailwayTransferDepartureCountries");
            DropTable("dbo.TourRailwayTransferDepartureCities");
            DropTable("dbo.TourRailwayTransferArrivalCountries");
            DropTable("dbo.TourRailwayTransfers");
            DropTable("dbo.TourRailwayTransferArrivalCities");
        }
    }
}
