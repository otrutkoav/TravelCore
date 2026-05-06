namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehiclePlans : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehiclePlans", "TransportId", "dbo.Transports");
            AlterColumn("dbo.VehiclePlans", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.VehiclePlans", "Comment", c => c.String(maxLength: 250));
            AddForeignKey("dbo.VehiclePlans", "TransportId", "dbo.Transports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehiclePlans", "TransportId", "dbo.Transports");
            AlterColumn("dbo.VehiclePlans", "Comment", c => c.String());
            AlterColumn("dbo.VehiclePlans", "Name", c => c.String());
            AddForeignKey("dbo.VehiclePlans", "TransportId", "dbo.Transports", "Id", cascadeDelete: true);
        }
    }
}
