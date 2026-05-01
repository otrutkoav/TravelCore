using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations
{
    public class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
        {
            ToTable("Regions");

            HasKey(x => x.Id);

            Property(x => x.CountryId)
                .IsRequired();

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            Property(x => x.SortOrder)
                .IsRequired();

            HasRequired(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);
        }
    }
}