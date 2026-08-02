using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Seating
{
    /// <summary>
    /// Конфигурация схем транспорта.
    /// </summary>
    public class VehiclePlanConfiguration : IEntityTypeConfiguration<VehiclePlan>
    {
        public void Configure(EntityTypeBuilder<VehiclePlan> builder)
        {
            builder.ToTable("VehiclePlans");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransportId)
                .IsRequired();

            builder.Property(x => x.RowsCount)
                .IsRequired();

            builder.Property(x => x.ColumnsCount)
                .IsRequired();

            builder.Property(x => x.AreaNumber)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(x => x.PlanOrientation)
                .IsRequired();

            builder.Property(x => x.IsAircraft)
                .IsRequired();

            builder.Property(x => x.Dates)
                .IsRequired(false);

            builder.Property(x => x.Comment)
                .IsRequired(false)
                .HasMaxLength(250);

            builder.HasOne(x => x.Transport)
                .WithMany()
                .HasForeignKey(x => x.TransportId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}