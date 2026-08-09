namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        NameEn = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                        IsDuration = c.Boolean(nullable: false),
                        IsRoute = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("CREATE UNIQUE INDEX [UX_ServiceTypes_Code] ON [dbo].[ServiceTypes] ([Code]) WHERE [Code] IS NOT NULL");
            
        }
        
        public override void Down()
        {
            Sql("DROP INDEX [UX_ServiceTypes_Code] ON [dbo].[ServiceTypes]");
            DropTable("dbo.ServiceTypes");
        }
    }
}
