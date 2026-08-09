namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourHotelCities : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TourHotels", "IX_TourHotels_Country_City");
            CreateTable(
                "dbo.TourHotelCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourHotelId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourHotels", t => t.TourHotelId, cascadeDelete: true)
                .Index(t => new { t.TourHotelId, t.CityId }, unique: true, name: "UX_TourHotelCities_TourHotel_City")
                .Index(t => t.CityId, name: "IX_TourHotelCities_City");
            
            CreateIndex("dbo.TourHotels", "CountryId", name: "IX_TourHotels_Country");
            DropColumn("dbo.TourHotels", "CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourHotels", "CityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TourHotelCities", "TourHotelId", "dbo.TourHotels");
            DropIndex("dbo.TourHotelCities", "IX_TourHotelCities_City");
            DropIndex("dbo.TourHotelCities", "UX_TourHotelCities_TourHotel_City");
            DropIndex("dbo.TourHotels", "IX_TourHotels_Country");
            DropTable("dbo.TourHotelCities");
            CreateIndex("dbo.TourHotels", new[] { "CountryId", "CityId" }, name: "IX_TourHotels_Country_City");
        }
    }
}
