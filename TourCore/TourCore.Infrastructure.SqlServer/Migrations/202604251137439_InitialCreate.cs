namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        NameEn = c.String(maxLength: 25),
                        Code = c.String(nullable: false, maxLength: 3),
                        IsoCode2 = c.String(nullable: false, maxLength: 2),
                        IsoCode3 = c.String(nullable: false, maxLength: 3),
                        DigitalCode = c.String(maxLength: 3),
                        CitizenshipName = c.String(maxLength: 50),
                        CitizenshipNameEn = c.String(maxLength: 50),
                        SortOrder = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Countries");
        }
    }
}
