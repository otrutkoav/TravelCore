namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResorts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        NameEn = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resorts", "CountryId", "dbo.Countries");
            DropIndex("dbo.Resorts", new[] { "CountryId" });
            DropTable("dbo.Resorts");
        }
    }
}
