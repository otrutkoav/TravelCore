namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBusTransfers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusTransfers",
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
            DropForeignKey("dbo.BusTransfers", "CountryToId", "dbo.Countries");
            DropForeignKey("dbo.BusTransfers", "CountryFromId", "dbo.Countries");
            DropForeignKey("dbo.BusTransfers", "CityToId", "dbo.Cities");
            DropForeignKey("dbo.BusTransfers", "CityFromId", "dbo.Cities");
            DropIndex("dbo.BusTransfers", new[] { "CityToId" });
            DropIndex("dbo.BusTransfers", new[] { "CountryToId" });
            DropIndex("dbo.BusTransfers", new[] { "CityFromId" });
            DropIndex("dbo.BusTransfers", new[] { "CountryFromId" });
            DropTable("dbo.BusTransfers");
        }
    }
}
