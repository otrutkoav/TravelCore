namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 40),
                        Name = c.String(nullable: false, maxLength: 150),
                        NameEn = c.String(maxLength: 100),
                        SortOrder = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoomCategories");
        }
    }
}
