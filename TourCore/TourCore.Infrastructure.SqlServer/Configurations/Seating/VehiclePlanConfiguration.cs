using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Seating
{
    /// <summary>
    /// Конфигурация схем транспорта.
    /// </summary>
    public class VehiclePlanConfiguration : EntityTypeConfiguration<VehiclePlan>
    {
        public VehiclePlanConfiguration()
        {
            ToTable("VehiclePlans");

            HasKey(x => x.Id);

            Property(x => x.TransportId)
                .IsRequired();

            Property(x => x.RowsCount)
                .IsRequired();

            Property(x => x.ColumnsCount)
                .IsRequired();

            Property(x => x.AreaNumber)
                .IsRequired();

            Property(x => x.Name)
                .IsOptional()
                .HasMaxLength(20);

            Property(x => x.PlanOrientation)
                .IsRequired();

            Property(x => x.IsAircraft)
                .IsRequired();

            Property(x => x.Dates)
                .IsOptional();

            Property(x => x.Comment)
                .IsOptional()
                .HasMaxLength(250);

            HasRequired(x => x.Transport)
                .WithMany()
                .HasForeignKey(x => x.TransportId)
                .WillCascadeOnDelete(false);

        }
    }
}