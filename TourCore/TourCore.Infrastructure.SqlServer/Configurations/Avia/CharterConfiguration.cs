using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Avia
{
    public class CharterConfiguration : EntityTypeConfiguration<Charter>
    {
        public CharterConfiguration()
        {
            ToTable("Charters");

            HasKey(x => x.Id);

            Property(x => x.DepartureCityId)
                .IsRequired();

            Property(x => x.DepartureAirportCode)
                .IsOptional()
                .HasMaxLength(4);

            Property(x => x.ArrivalCityId)
                .IsRequired();

            Property(x => x.ArrivalAirportCode)
                .IsOptional()
                .HasMaxLength(4);

            Property(x => x.AirlineCode)
                .IsOptional()
                .HasMaxLength(3);

            Property(x => x.FlightNumber)
                .IsRequired()
                .HasMaxLength(4);

            Property(x => x.AircraftCode)
                .IsOptional()
                .HasMaxLength(3);

            Property(x => x.AirClassCode)
                .IsOptional()
                .HasMaxLength(10);

            Property(x => x.StopsCount)
                .IsOptional();

            Property(x => x.TimeChangesCode)
                .IsOptional()
                .HasMaxLength(1);

            HasRequired(x => x.DepartureCity)
                .WithMany()
                .HasForeignKey(x => x.DepartureCityId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.ArrivalCity)
                .WithMany()
                .HasForeignKey(x => x.ArrivalCityId)
                .WillCascadeOnDelete(false);
        }
    }
}