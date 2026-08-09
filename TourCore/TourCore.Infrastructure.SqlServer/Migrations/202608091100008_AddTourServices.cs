namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourServices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        OrderIndex = c.Int(nullable: false),
                        ParentServiceId = c.Int(),
                        Revision = c.Long(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourServices", t => t.ParentServiceId)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => new { t.TourId, t.OrderIndex }, name: "IX_TourServices_Tour_Order")
                .Index(t => t.ServiceTypeId)
                .Index(t => t.ParentServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourServices", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourServices", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.TourServices", "ParentServiceId", "dbo.TourServices");
            DropIndex("dbo.TourServices", new[] { "ParentServiceId" });
            DropIndex("dbo.TourServices", new[] { "ServiceTypeId" });
            DropIndex("dbo.TourServices", "IX_TourServices_Tour_Order");
            DropTable("dbo.TourServices");
        }
    }
}
