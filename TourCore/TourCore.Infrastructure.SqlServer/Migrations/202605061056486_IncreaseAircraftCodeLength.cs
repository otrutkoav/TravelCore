namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseAircraftCodeLength : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Aircraft", new[] { "Code" });
            AlterColumn("dbo.Aircraft", "Code", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Aircraft", "Code", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Aircraft", new[] { "Code" });
            AlterColumn("dbo.Aircraft", "Code", c => c.String(nullable: false, maxLength: 3));
            CreateIndex("dbo.Aircraft", "Code", unique: true);
        }
    }
}
