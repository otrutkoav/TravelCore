namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHotels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        ResortId = c.Int(),
                        CategoryId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 200),
                        NameEn = c.String(maxLength: 200),
                        Stars = c.String(nullable: false, maxLength: 20),
                        Code = c.String(nullable: false, maxLength: 10),
                        Address = c.String(maxLength: 254),
                        Phone = c.String(maxLength: 50),
                        Fax = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Website = c.String(maxLength: 254),
                        Latitude = c.String(maxLength: 30),
                        Longitude = c.String(maxLength: 30),
                        IsCruise = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Rank = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HotelCategories", t => t.CategoryId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Resorts", t => t.ResortId)
                .Index(t => t.CountryId)
                .Index(t => t.CityId)
                .Index(t => t.ResortId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "ResortId", "dbo.Resorts");
            DropForeignKey("dbo.Hotels", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Hotels", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Hotels", "CategoryId", "dbo.HotelCategories");
            DropIndex("dbo.Hotels", new[] { "CategoryId" });
            DropIndex("dbo.Hotels", new[] { "ResortId" });
            DropIndex("dbo.Hotels", new[] { "CityId" });
            DropIndex("dbo.Hotels", new[] { "CountryId" });
            DropTable("dbo.Hotels");
        }
    }
}
