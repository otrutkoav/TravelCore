using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Seating.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Seating
{
    public class SeatingCellConfiguration : EntityTypeConfiguration<SeatingCell>
    {
        public SeatingCellConfiguration()
        {
            ToTable("SeatingCells");

            HasKey(x => x.Id);

            Property(x => x.VehiclePlanId)
                .IsRequired();

            Property(x => x.Number)
                .IsOptional()
                .HasMaxLength(4);

            Property(x => x.Type)
                .IsOptional();

            Property(x => x.SeatsCount)
                .IsOptional();

            Property(x => x.Index)
                .IsRequired();

            Property(x => x.Border)
                .IsOptional()
                .HasMaxLength(4);

            HasRequired(x => x.VehiclePlan)
                .WithMany()
                .HasForeignKey(x => x.VehiclePlanId)
                .WillCascadeOnDelete(false);
        }
    }
}