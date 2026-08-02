using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CountryId)
                .IsRequired();

            builder.Property(x => x.RegionId)
                .IsRequired(false);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.Property(x => x.IsDeparturePoint)
                .IsRequired();

            builder.Property(x => x.TimeZone)
                .IsRequired(false)
                .HasMaxLength(150);

            builder.Property(x => x.IataCode)
                .IsRequired(false)
                .HasMaxLength(3);

            builder.Property(x => x.Coordinates)
                .IsRequired(false)
                .HasMaxLength(30);

            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Region)
                .WithMany()
                .HasForeignKey(x => x.RegionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}