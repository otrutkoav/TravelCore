using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Avia
{
    public class AirClassConfiguration : IEntityTypeConfiguration<AirClass>
    {
        public void Configure(EntityTypeBuilder<AirClass> builder)
        {
            builder.ToTable("AirClasses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.Group)
                .IsRequired(false)
                .HasMaxLength(128);

            builder.Property(x => x.SortOrder)
                .IsRequired();
        }
    }
}