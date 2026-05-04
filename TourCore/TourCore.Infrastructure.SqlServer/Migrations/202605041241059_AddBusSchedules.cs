namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBusSchedules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusTransferId = c.Int(nullable: false),
                        DateFrom = c.DateTime(),
                        DateTo = c.DateTime(),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        DaysOfWeek = c.String(maxLength: 7),
                        DaysOnRoad = c.Short(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusTransfers", t => t.BusTransferId)
                .Index(t => t.BusTransferId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusSchedules", "BusTransferId", "dbo.BusTransfers");
            DropIndex("dbo.BusSchedules", new[] { "BusTransferId" });
            DropTable("dbo.BusSchedules");
        }
    }
}
