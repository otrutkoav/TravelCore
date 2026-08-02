using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Avia
{
    public class CharterConfiguration : IEntityTypeConfiguration<Charter>
    {
        public void Configure(EntityTypeBuilder<Charter> builder)
        {
            builder.ToTable("Charters");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DepartureCityId)
                .IsRequired();

            builder.Property(x => x.DepartureAirportCode)
                .IsRequired(false)
                .HasMaxLength(4);

            builder.Property(x => x.ArrivalCityId)
                .IsRequired();

            builder.Property(x => x.ArrivalAirportCode)
                .IsRequired(false)
                .HasMaxLength(4);

            builder.Property(x => x.AirlineCode)
                .IsRequired(false)
                .HasMaxLength(3);

            builder.Property(x => x.FlightNumber)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(x => x.AircraftCode)
                .IsRequired(false)
                .HasMaxLength(3);

            builder.Property(x => x.AirClassCode)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(x => x.StopsCount)
                .IsRequired(false);

            builder.Property(x => x.TimeChangesCode)
                .IsRequired(false)
                .HasMaxLength(1);

            builder.HasOne(x => x.DepartureCity)
                .WithMany()
                .HasForeignKey(x => x.DepartureCityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ArrivalCity)
                .WithMany()
                .HasForeignKey(x => x.ArrivalCityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}