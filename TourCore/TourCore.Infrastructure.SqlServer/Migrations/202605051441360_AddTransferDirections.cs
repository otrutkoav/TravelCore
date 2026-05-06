namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransferDirections : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransferDirections", "Name", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransferDirections", "Name", c => c.String());
        }
    }
}
