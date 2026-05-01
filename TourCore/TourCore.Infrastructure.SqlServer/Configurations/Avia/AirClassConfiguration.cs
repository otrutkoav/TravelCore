using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Avia
{
    public class AirClassConfiguration : EntityTypeConfiguration<AirClass>
    {
        public AirClassConfiguration()
        {
            ToTable("AirClasses");

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

            Property(x => x.Group)
                .IsOptional()
                .HasMaxLength(128);

            Property(x => x.SortOrder)
                .IsRequired();
        }
    }
}