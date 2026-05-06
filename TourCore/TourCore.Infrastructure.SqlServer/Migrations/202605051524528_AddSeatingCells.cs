namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeatingCells : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SeatingCells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehiclePlanId = c.Int(nullable: false),
                        Number = c.String(maxLength: 4),
                        Type = c.Short(),
                        SeatsCount = c.Short(),
                        Index = c.Int(nullable: false),
                        Border = c.String(maxLength: 4),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        VehiclePlan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehiclePlans", t => t.VehiclePlan_Id)
                .ForeignKey("dbo.VehiclePlans", t => t.VehiclePlanId)
                .Index(t => t.VehiclePlanId)
                .Index(t => t.VehiclePlan_Id);
            
            CreateTable(
                "dbo.VehiclePlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransportId = c.Int(nullable: false),
                        RowsCount = c.Int(nullable: false),
                        ColumnsCount = c.Int(nullable: false),
                        AreaNumber = c.Int(nullable: false),
                        Name = c.String(),
                        PlanOrientation = c.Boolean(nullable: false),
                        IsAircraft = c.Boolean(nullable: false),
                        Dates = c.String(),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transports", t => t.TransportId, cascadeDelete: true)
                .Index(t => t.TransportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeatingCells", "VehiclePlanId", "dbo.VehiclePlans");
            DropForeignKey("dbo.VehiclePlans", "TransportId", "dbo.Transports");
            DropForeignKey("dbo.SeatingCells", "VehiclePlan_Id", "dbo.VehiclePlans");
            DropIndex("dbo.VehiclePlans", new[] { "TransportId" });
            DropIndex("dbo.SeatingCells", new[] { "VehiclePlan_Id" });
            DropIndex("dbo.SeatingCells", new[] { "VehiclePlanId" });
            DropTable("dbo.VehiclePlans");
            DropTable("dbo.SeatingCells");
        }
    }
}
