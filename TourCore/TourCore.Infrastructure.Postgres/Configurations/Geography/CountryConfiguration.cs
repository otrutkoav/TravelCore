using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(25);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.IsoCode2)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(x => x.IsoCode3)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.DigitalCode)
                .IsRequired(false)
                .HasMaxLength(3);

            builder.Property(x => x.CitizenshipName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.CitizenshipNameEn)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.SortOrder)
                .IsRequired();
        }
    }
}