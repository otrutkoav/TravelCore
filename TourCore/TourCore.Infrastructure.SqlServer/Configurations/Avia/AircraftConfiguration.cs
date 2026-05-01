using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.Persistence.Configurations.Avia
{
    public class AircraftConfiguration : EntityTypeConfiguration<Aircraft>
    {
        public AircraftConfiguration()
        {
            ToTable("Aircraft");

            HasKey(x => x.Id);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}