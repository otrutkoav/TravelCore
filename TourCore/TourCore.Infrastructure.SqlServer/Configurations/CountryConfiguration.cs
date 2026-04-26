using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            ToTable("Countries");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(25);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.IsoCode2)
                .IsRequired()
                .HasMaxLength(2);

            Property(x => x.IsoCode3)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.DigitalCode)
                .IsOptional()
                .HasMaxLength(3);

            Property(x => x.CitizenshipName)
                .IsOptional()
                .HasMaxLength(50);

            Property(x => x.CitizenshipNameEn)
                .IsOptional()
                .HasMaxLength(50);

            Property(x => x.SortOrder)
                .IsRequired();
        }
    }
}