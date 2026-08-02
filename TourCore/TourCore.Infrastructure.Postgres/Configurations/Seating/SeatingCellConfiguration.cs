using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Seating
{
    public class SeatingCellConfiguration : IEntityTypeConfiguration<SeatingCell>
    {
        public void Configure(EntityTypeBuilder<SeatingCell> builder)
        {
            builder.ToTable("SeatingCells");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VehiclePlanId)
                .IsRequired();

            builder.Property(x => x.Number)
                .IsRequired(false)
                .HasMaxLength(4);

            builder.Property(x => x.Type)
                .IsRequired(false);

            builder.Property(x => x.SeatsCount)
                .IsRequired(false);

            builder.Property(x => x.Index)
                .IsRequired();

            builder.Property(x => x.Border)
                .IsRequired(false)
                .HasMaxLength(4);

            builder.HasOne(x => x.VehiclePlan)
                .WithMany(x => x.Cells)
                .HasForeignKey(x => x.VehiclePlanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
