namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCharters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Charters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartureCityId = c.Int(nullable: false),
                        DepartureAirportCode = c.String(maxLength: 4),
                        ArrivalCityId = c.Int(nullable: false),
                        ArrivalAirportCode = c.String(maxLength: 4),
                        AirlineCode = c.String(maxLength: 3),
                        FlightNumber = c.String(nullable: false, maxLength: 4),
                        AircraftCode = c.String(maxLength: 3),
                        AirClassCode = c.String(maxLength: 10),
                        StopsCount = c.Short(),
                        TimeChangesCode = c.String(maxLength: 1),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.ArrivalCityId)
                .ForeignKey("dbo.Cities", t => t.DepartureCityId)
                .Index(t => t.DepartureCityId)
                .Index(t => t.ArrivalCityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Charters", "DepartureCityId", "dbo.Cities");
            DropForeignKey("dbo.Charters", "ArrivalCityId", "dbo.Cities");
            DropIndex("dbo.Charters", new[] { "ArrivalCityId" });
            DropIndex("dbo.Charters", new[] { "DepartureCityId" });
            DropTable("dbo.Charters");
        }
    }
}
