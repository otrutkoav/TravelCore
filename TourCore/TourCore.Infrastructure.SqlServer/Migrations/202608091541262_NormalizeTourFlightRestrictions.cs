namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NormalizeTourFlightRestrictions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourFlightRestrictions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourFlightId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        ReferenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourFlights", t => t.TourFlightId, cascadeDelete: true);

            CreateIndex(
                "dbo.TourFlightRestrictions",
                new[] { "TourFlightId", "Type", "ReferenceId" },
                unique: true,
                name: "UX_TourFlightRestrictions_Flight_Type_Reference");
            CreateIndex(
                "dbo.TourFlightRestrictions",
                new[] { "Type", "ReferenceId" },
                name: "IX_TourFlightRestrictions_Type_Reference");
            
            DropColumn("dbo.TourFlights", "DepartureCountryIds");
            DropColumn("dbo.TourFlights", "DepartureCityIds");
            DropColumn("dbo.TourFlights", "ArrivalCountryIds");
            DropColumn("dbo.TourFlights", "ArrivalCityIds");
            DropColumn("dbo.TourFlights", "AirlineIds");
            DropColumn("dbo.TourFlights", "FlightIds");
            DropColumn("dbo.TourFlights", "DepartureAirportIds");
            DropColumn("dbo.TourFlights", "ArrivalAirportIds");
            DropColumn("dbo.TourFlights", "FareIds");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourFlights", "FareIds", c => c.String());
            AddColumn("dbo.TourFlights", "ArrivalAirportIds", c => c.String());
            AddColumn("dbo.TourFlights", "DepartureAirportIds", c => c.String());
            AddColumn("dbo.TourFlights", "FlightIds", c => c.String());
            AddColumn("dbo.TourFlights", "AirlineIds", c => c.String());
            AddColumn("dbo.TourFlights", "ArrivalCityIds", c => c.String());
            AddColumn("dbo.TourFlights", "ArrivalCountryIds", c => c.String());
            AddColumn("dbo.TourFlights", "DepartureCityIds", c => c.String());
            AddColumn("dbo.TourFlights", "DepartureCountryIds", c => c.String());
            DropForeignKey("dbo.TourFlightRestrictions", "TourFlightId", "dbo.TourFlights");
            DropIndex("dbo.TourFlightRestrictions", "IX_TourFlightRestrictions_Type_Reference");
            DropIndex("dbo.TourFlightRestrictions", "UX_TourFlightRestrictions_Flight_Type_Reference");
            DropTable("dbo.TourFlightRestrictions");
        }
    }
}
