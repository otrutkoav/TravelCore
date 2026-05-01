using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            ToTable("Cities");

            HasKey(x => x.Id);

            Property(x => x.CountryId)
                .IsRequired();

            Property(x => x.RegionId)
                .IsOptional();

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.SortOrder)
                .IsRequired();

            Property(x => x.IsDeparturePoint)
                .IsRequired();

            Property(x => x.TimeZone)
                .IsOptional()
                .HasMaxLength(150);

            Property(x => x.IataCode)
                .IsOptional()
                .HasMaxLength(3);

            Property(x => x.Coordinates)
                .IsOptional()
                .HasMaxLength(30);

            HasRequired(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Region)
                .WithMany()
                .HasForeignKey(x => x.RegionId)
                .WillCascadeOnDelete(false);
        }
    }
}