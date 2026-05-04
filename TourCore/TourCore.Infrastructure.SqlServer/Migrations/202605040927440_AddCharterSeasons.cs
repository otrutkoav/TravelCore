namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCharterSeasons : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharterSeasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CharterId = c.Int(nullable: false),
                        DateFrom = c.DateTime(),
                        DateTo = c.DateTime(),
                        DaysOfWeek = c.String(maxLength: 7),
                        TimeFrom = c.DateTime(),
                        TimeTo = c.DateTime(),
                        IsNextDayArrival = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 20),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CharterSeasons");
        }
    }
}
