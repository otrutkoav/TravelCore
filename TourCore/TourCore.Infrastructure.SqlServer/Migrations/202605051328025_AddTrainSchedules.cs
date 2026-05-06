namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainSchedules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RailwayTransferId = c.Int(nullable: false),
                        DateFrom = c.DateTime(),
                        DateTo = c.DateTime(),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        DaysOfWeek = c.String(maxLength: 7),
                        DaysOnRoad = c.Short(),
                        Remark = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RailwayTransfers", t => t.RailwayTransferId)
                .Index(t => t.RailwayTransferId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainSchedules", "RailwayTransferId", "dbo.RailwayTransfers");
            DropIndex("dbo.TrainSchedules", new[] { "RailwayTransferId" });
            DropTable("dbo.TrainSchedules");
        }
    }
}
