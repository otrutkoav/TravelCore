namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHotelCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        PrintOrder = c.Int(),
                        GlobalCode = c.String(maxLength: 20),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HotelCategories");
        }
    }
}
