using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Avia
{
    public class AirlineConfiguration : EntityTypeConfiguration<Airline>
    {
        public AirlineConfiguration()
        {
            ToTable("Airlines");

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

            Property(x => x.IcaoCode)
                .IsOptional()
                .HasMaxLength(4);
        }
    }
}