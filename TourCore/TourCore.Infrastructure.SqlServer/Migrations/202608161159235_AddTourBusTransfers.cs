namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourBusTransfers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourBusTransferArrivalCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.CityId }, unique: true, name: "UX_TourBusTransferArrivalCities_Transfer_City")
                .Index(t => t.CityId, name: "IX_TourBusTransferArrivalCities_City");
            
            CreateTable(
                "dbo.TourBusTransfers",
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
                .Index(t => t.TourServiceId, unique: true, name: "UX_TourBusTransfers_TourService");
            
            CreateTable(
                "dbo.TourBusTransferArrivalCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.CountryId }, unique: true, name: "UX_TourBusTransferArrivalCountries_Transfer_Country")
                .Index(t => t.CountryId, name: "IX_TourBusTransferArrivalCountries_Country");
            
            CreateTable(
                "dbo.TourBusTransferBoardingPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        BoardingPointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.BoardingPointId }, unique: true, name: "UX_TourBusTransferBoardingPoints_Transfer_Point")
                .Index(t => t.BoardingPointId, name: "IX_TourBusTransferBoardingPoints_Point");
            
            CreateTable(
                "dbo.TourBusTransferDepartureCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.CityId }, unique: true, name: "UX_TourBusTransferDepartureCities_Transfer_City")
                .Index(t => t.CityId, name: "IX_TourBusTransferDepartureCities_City");
            
            CreateTable(
                "dbo.TourBusTransferDepartureCountries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.CountryId }, unique: true, name: "UX_TourBusTransferDepartureCountries_Transfer_Country")
                .Index(t => t.CountryId, name: "IX_TourBusTransferDepartureCountries_Country");
            
            CreateTable(
                "dbo.TourBusTransferOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        BusTransferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.BusTransferId }, unique: true, name: "UX_TourBusTransferOptions_Transfer_Option")
                .Index(t => t.BusTransferId, name: "IX_TourBusTransferOptions_BusTransfer");
            
            CreateTable(
                "dbo.TourBusTransferTransports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourBusTransferId = c.Int(nullable: false),
                        TransportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourBusTransfers", t => t.TourBusTransferId, cascadeDelete: true)
                .Index(t => new { t.TourBusTransferId, t.TransportId }, unique: true, name: "UX_TourBusTransferTransports_Transfer_Transport")
                .Index(t => t.TransportId, name: "IX_TourBusTransferTransports_Transport");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourBusTransferArrivalCities", "TourBusTransferId", "dbo.TourBusTransfers");
            DropForeignKey("dbo.TourBusTransferTransports", "TourBusTransferId", "dbo.TourBusTransfers");
            DropForeignKey("dbo.TourBusTransfers", "TourServiceId", "dbo.TourServices");
            DropForeignKey("dbo.TourBusTransferOptions", "TourBusTransferId", "dbo.TourBusTransfers");
            DropForeignKey("dbo.TourBusTransferDepartureCountries", "TourBusTransferId", "dbo.TourBusTransfers");
            DropForeignKey("dbo.TourBusTransferDepartureCities", "TourBusTransferId", "dbo.TourBusTransfers");
            DropForeignKey("dbo.TourBusTransferBoardingPoints", "TourBusTransferId", "dbo.TourBusTransfers");
            DropForeignKey("dbo.TourBusTransferArrivalCountries", "TourBusTransferId", "dbo.TourBusTransfers");
            DropIndex("dbo.TourBusTransferTransports", "IX_TourBusTransferTransports_Transport");
            DropIndex("dbo.TourBusTransferTransports", "UX_TourBusTransferTransports_Transfer_Transport");
            DropIndex("dbo.TourBusTransferOptions", "IX_TourBusTransferOptions_BusTransfer");
            DropIndex("dbo.TourBusTransferOptions", "UX_TourBusTransferOptions_Transfer_Option");
            DropIndex("dbo.TourBusTransferDepartureCountries", "IX_TourBusTransferDepartureCountries_Country");
            DropIndex("dbo.TourBusTransferDepartureCountries", "UX_TourBusTransferDepartureCountries_Transfer_Country");
            DropIndex("dbo.TourBusTransferDepartureCities", "IX_TourBusTransferDepartureCities_City");
            DropIndex("dbo.TourBusTransferDepartureCities", "UX_TourBusTransferDepartureCities_Transfer_City");
            DropIndex("dbo.TourBusTransferBoardingPoints", "IX_TourBusTransferBoardingPoints_Point");
            DropIndex("dbo.TourBusTransferBoardingPoints", "UX_TourBusTransferBoardingPoints_Transfer_Point");
            DropIndex("dbo.TourBusTransferArrivalCountries", "IX_TourBusTransferArrivalCountries_Country");
            DropIndex("dbo.TourBusTransferArrivalCountries", "UX_TourBusTransferArrivalCountries_Transfer_Country");
            DropIndex("dbo.TourBusTransfers", "UX_TourBusTransfers_TourService");
            DropIndex("dbo.TourBusTransferArrivalCities", "IX_TourBusTransferArrivalCities_City");
            DropIndex("dbo.TourBusTransferArrivalCities", "UX_TourBusTransferArrivalCities_Transfer_City");
            DropTable("dbo.TourBusTransferTransports");
            DropTable("dbo.TourBusTransferOptions");
            DropTable("dbo.TourBusTransferDepartureCountries");
            DropTable("dbo.TourBusTransferDepartureCities");
            DropTable("dbo.TourBusTransferBoardingPoints");
            DropTable("dbo.TourBusTransferArrivalCountries");
            DropTable("dbo.TourBusTransfers");
            DropTable("dbo.TourBusTransferArrivalCities");
        }
    }
}
