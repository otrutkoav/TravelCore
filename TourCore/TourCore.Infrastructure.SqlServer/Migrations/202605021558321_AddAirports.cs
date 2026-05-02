namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAirports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 100),
                        NameEn = c.String(maxLength: 100),
                        IcaoCode = c.String(maxLength: 5),
                        LetterCode = c.String(maxLength: 1),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Airports", "CityId", "dbo.Cities");
            DropIndex("dbo.Airports", new[] { "CityId" });
            DropTable("dbo.Airports");
        }
    }
}
