namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransfers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransferDirections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        NameEn = c.String(maxLength: 100),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        DurationText = c.String(maxLength: 5),
                        PlaceFrom = c.String(maxLength: 300),
                        PlaceTo = c.String(maxLength: 300),
                        IsMain = c.Boolean(nullable: false),
                        CityId = c.Int(),
                        DirectionId = c.Int(),
                        Url = c.String(maxLength: 192),
                        ShowOrder = c.Int(nullable: false),
                        AutoApplyFrom = c.Boolean(nullable: false),
                        AutoApplyTo = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.TransferDirections", t => t.DirectionId)
                .Index(t => t.CityId)
                .Index(t => t.DirectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transfers", "DirectionId", "dbo.TransferDirections");
            DropForeignKey("dbo.Transfers", "CityId", "dbo.Cities");
            DropIndex("dbo.Transfers", new[] { "DirectionId" });
            DropIndex("dbo.Transfers", new[] { "CityId" });
            DropTable("dbo.Transfers");
            DropTable("dbo.TransferDirections");
        }
    }
}
