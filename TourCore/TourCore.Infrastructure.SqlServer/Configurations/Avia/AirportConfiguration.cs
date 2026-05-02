using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Avia
{
    public class AirportConfiguration : EntityTypeConfiguration<Airport>
    {
        public AirportConfiguration()
        {
            ToTable("Airports");

            HasKey(x => x.Id);

            Property(x => x.CityId)
                .IsRequired();

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(5);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.IcaoCode)
                .IsOptional()
                .HasMaxLength(5);

            Property(x => x.LetterCode)
                .IsOptional()
                .HasMaxLength(1);

            HasRequired(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);
        }
    }
}