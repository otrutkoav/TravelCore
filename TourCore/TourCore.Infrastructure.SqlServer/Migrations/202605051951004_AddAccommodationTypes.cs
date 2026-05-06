namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccommodationTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationPlacementRules",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AdultsCount = c.Short(nullable: false),
                    ChildrenCount = c.Short(nullable: false),
                    ChildrenAreInfants = c.Boolean(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AccommodationPlacementRuleAgeRanges",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AccommodationPlacementRuleId = c.Int(nullable: false),
                    AgeFrom = c.Short(nullable: false),
                    AgeTo = c.Short(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccommodationPlacementRules", t => t.AccommodationPlacementRuleId)
                .Index(t => t.AccommodationPlacementRuleId);

            CreateTable(
                "dbo.AccommodationTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 50),
                    Name = c.String(nullable: false, maxLength: 100),
                    NameEn = c.String(maxLength: 100),
                    IsMain = c.Boolean(nullable: false),
                    AgeFrom = c.Short(),
                    AgeTo = c.Short(),
                    PerRoom = c.Short(),
                    SortOrder = c.Int(nullable: false),
                    Description = c.String(),
                    MainPlacementRuleId = c.Int(),
                    ExtraPlacementRuleId = c.Int(),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccommodationPlacementRules", t => t.MainPlacementRuleId)
                .ForeignKey("dbo.AccommodationPlacementRules", t => t.ExtraPlacementRuleId)
                .Index(t => t.MainPlacementRuleId)
                .Index(t => t.ExtraPlacementRuleId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AccommodationTypes", "MainPlacementRuleId", "dbo.AccommodationPlacementRules");
            DropForeignKey("dbo.AccommodationTypes", "ExtraPlacementRuleId", "dbo.AccommodationPlacementRules");
            DropForeignKey("dbo.AccommodationPlacementRuleAgeRanges", "AccommodationPlacementRuleId", "dbo.AccommodationPlacementRules");

            DropIndex("dbo.AccommodationTypes", new[] { "ExtraPlacementRuleId" });
            DropIndex("dbo.AccommodationTypes", new[] { "MainPlacementRuleId" });
            DropIndex("dbo.AccommodationPlacementRuleAgeRanges", new[] { "AccommodationPlacementRuleId" });

            DropTable("dbo.AccommodationTypes");
            DropTable("dbo.AccommodationPlacementRuleAgeRanges");
            DropTable("dbo.AccommodationPlacementRules");
        }
    }
}
