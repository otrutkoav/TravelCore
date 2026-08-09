namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourFlights : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourFlightGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourServiceId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Priority = c.Int(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourServices", t => t.TourServiceId, cascadeDelete: true)
                .Index(t => t.TourServiceId);
            
            CreateTable(
                "dbo.TourFlights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourFlightGroupId = c.Int(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                        DepartureCountryIds = c.String(),
                        DepartureCityIds = c.String(),
                        ArrivalCountryIds = c.String(),
                        ArrivalCityIds = c.String(),
                        AirlineIds = c.String(),
                        FlightIds = c.String(),
                        DepartureAirportIds = c.String(),
                        ArrivalAirportIds = c.String(),
                        FareIds = c.String(),
                        MinConnectionMinutes = c.Int(),
                        MaxConnectionMinutes = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourFlightGroups", t => t.TourFlightGroupId, cascadeDelete: true)
                .Index(t => t.TourFlightGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourFlightGroups", "TourServiceId", "dbo.TourServices");
            DropForeignKey("dbo.TourFlights", "TourFlightGroupId", "dbo.TourFlightGroups");
            DropIndex("dbo.TourFlights", new[] { "TourFlightGroupId" });
            DropIndex("dbo.TourFlightGroups", new[] { "TourServiceId" });
            DropTable("dbo.TourFlights");
            DropTable("dbo.TourFlightGroups");
        }
    }
}
