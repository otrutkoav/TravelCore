namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRealCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromRateCode = c.String(nullable: false, maxLength: 3),
                        ToRateCode = c.String(nullable: false, maxLength: 3),
                        Course = c.Decimal(precision: 18, scale: 6),
                        CentralBankCourse = c.Decimal(precision: 18, scale: 6),
                        DateBeg = c.DateTime(),
                        DateEnd = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RealCourses");
        }
    }
}
