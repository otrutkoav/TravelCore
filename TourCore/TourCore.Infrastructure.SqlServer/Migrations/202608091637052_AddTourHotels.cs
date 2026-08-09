namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourHotels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourHotelGroups",
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
                .Index(t => new { t.TourServiceId, t.OrderIndex }, unique: true, name: "UX_TourHotelGroups_Service_Order");
            
            CreateTable(
                "dbo.TourHotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourHotelGroupId = c.Int(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourHotelGroups", t => t.TourHotelGroupId, cascadeDelete: true)
                .Index(t => new { t.TourHotelGroupId, t.OrderIndex }, unique: true, name: "UX_TourHotels_Group_Order")
                .Index(t => new { t.CountryId, t.CityId }, name: "IX_TourHotels_Country_City");
            
            CreateTable(
                "dbo.TourHotelAllowedHotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourHotelId = c.Int(nullable: false),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourHotels", t => t.TourHotelId, cascadeDelete: true)
                .Index(t => new { t.TourHotelId, t.HotelId }, unique: true, name: "UX_TourHotelAllowedHotels_TourHotel_Hotel")
                .Index(t => t.HotelId, name: "IX_TourHotelAllowedHotels_Hotel");
            
            CreateTable(
                "dbo.TourHotelDurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourHotelId = c.Int(nullable: false),
                        Nights = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourHotels", t => t.TourHotelId, cascadeDelete: true)
                .Index(t => new { t.TourHotelId, t.Nights }, unique: true, name: "UX_TourHotelDurations_Hotel_Nights");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourHotelGroups", "TourServiceId", "dbo.TourServices");
            DropForeignKey("dbo.TourHotels", "TourHotelGroupId", "dbo.TourHotelGroups");
            DropForeignKey("dbo.TourHotelDurations", "TourHotelId", "dbo.TourHotels");
            DropForeignKey("dbo.TourHotelAllowedHotels", "TourHotelId", "dbo.TourHotels");
            DropIndex("dbo.TourHotelDurations", "UX_TourHotelDurations_Hotel_Nights");
            DropIndex("dbo.TourHotelAllowedHotels", "IX_TourHotelAllowedHotels_Hotel");
            DropIndex("dbo.TourHotelAllowedHotels", "UX_TourHotelAllowedHotels_TourHotel_Hotel");
            DropIndex("dbo.TourHotels", "IX_TourHotels_Country_City");
            DropIndex("dbo.TourHotels", "UX_TourHotels_Group_Order");
            DropIndex("dbo.TourHotelGroups", "UX_TourHotelGroups_Service_Order");
            DropTable("dbo.TourHotelDurations");
            DropTable("dbo.TourHotelAllowedHotels");
            DropTable("dbo.TourHotels");
            DropTable("dbo.TourHotelGroups");
        }
    }
}
