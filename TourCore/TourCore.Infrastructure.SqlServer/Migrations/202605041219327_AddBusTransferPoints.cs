namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBusTransferPoints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusTransferPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusTransferId = c.Int(nullable: false),
                        CountryFromId = c.Int(nullable: false),
                        CityFromId = c.Int(nullable: false),
                        CountryToId = c.Int(nullable: false),
                        CityToId = c.Int(nullable: false),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        DayFrom = c.Short(),
                        DayTo = c.Short(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusTransfers", t => t.BusTransferId)
                .ForeignKey("dbo.Cities", t => t.CityFromId)
                .ForeignKey("dbo.Cities", t => t.CityToId)
                .ForeignKey("dbo.Countries", t => t.CountryFromId)
                .ForeignKey("dbo.Countries", t => t.CountryToId)
                .Index(t => t.BusTransferId)
                .Index(t => t.CountryFromId)
                .Index(t => t.CityFromId)
                .Index(t => t.CountryToId)
                .Index(t => t.CityToId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusTransferPoints", "CountryToId", "dbo.Countries");
            DropForeignKey("dbo.BusTransferPoints", "CountryFromId", "dbo.Countries");
            DropForeignKey("dbo.BusTransferPoints", "CityToId", "dbo.Cities");
            DropForeignKey("dbo.BusTransferPoints", "CityFromId", "dbo.Cities");
            DropForeignKey("dbo.BusTransferPoints", "BusTransferId", "dbo.BusTransfers");
            DropIndex("dbo.BusTransferPoints", new[] { "CityToId" });
            DropIndex("dbo.BusTransferPoints", new[] { "CountryToId" });
            DropIndex("dbo.BusTransferPoints", new[] { "CityFromId" });
            DropIndex("dbo.BusTransferPoints", new[] { "CountryFromId" });
            DropIndex("dbo.BusTransferPoints", new[] { "BusTransferId" });
            DropTable("dbo.BusTransferPoints");
        }
    }
}
