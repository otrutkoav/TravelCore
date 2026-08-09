namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTours : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourAvailableDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => new { t.TourId, t.Date }, unique: true, name: "UX_TourAvailableDates_Tour_Date");
            
            // EF6 cannot configure an identity seed through the fluent model. Keep the
            // legacy-compatible tour range in the migration itself.
            Sql(@"CREATE TABLE [dbo].[Tours](
                    [Id] int IDENTITY(100000000,1) NOT NULL,
                    [Name] nvarchar(128) NOT NULL,
                    [Status] int NOT NULL,
                    [CurrencyId] int NOT NULL,
                    [DateMode] int NOT NULL,
                    [AvailableDateFrom] date NULL,
                    [AvailableDateTo] date NULL,
                    [DescriptionUrl] nvarchar(2048) NULL,
                    [DescriptionHtml] nvarchar(max) NULL,
                    [BookingConditionsHtml] nvarchar(max) NULL,
                    [CreatedAt] datetime NOT NULL,
                    [UpdatedAt] datetime NULL,
                    CONSTRAINT [PK_dbo.Tours] PRIMARY KEY ([Id])
                )");
            CreateIndex("dbo.Tours", "CurrencyId");
            AddForeignKey("dbo.Tours", "CurrencyId", "dbo.Rates", "Id");
            
            CreateTable(
                "dbo.TourTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        ParentId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourTypes", t => t.ParentId)
                .Index(t => t.Name, unique: true, name: "UX_TourTypes_Name")
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.TourTourTypes",
                c => new
                    {
                        TourTypeId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TourTypeId, t.TourId })
                .ForeignKey("dbo.TourTypes", t => t.TourTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourTypeId)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourTourTypes", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourTourTypes", "TourTypeId", "dbo.TourTypes");
            DropForeignKey("dbo.TourTypes", "ParentId", "dbo.TourTypes");
            DropForeignKey("dbo.Tours", "CurrencyId", "dbo.Rates");
            DropForeignKey("dbo.TourAvailableDates", "TourId", "dbo.Tours");
            DropIndex("dbo.TourTourTypes", new[] { "TourId" });
            DropIndex("dbo.TourTourTypes", new[] { "TourTypeId" });
            DropIndex("dbo.TourTypes", new[] { "ParentId" });
            DropIndex("dbo.TourTypes", "UX_TourTypes_Name");
            DropIndex("dbo.Tours", new[] { "CurrencyId" });
            DropIndex("dbo.TourAvailableDates", "UX_TourAvailableDates_Tour_Date");
            DropTable("dbo.TourTourTypes");
            DropTable("dbo.TourTypes");
            DropTable("dbo.Tours");
            DropTable("dbo.TourAvailableDates");
        }
    }
}
