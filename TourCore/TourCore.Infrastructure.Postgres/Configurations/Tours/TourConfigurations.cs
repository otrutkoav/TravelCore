using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Finance.Entities;
using TourCore.Domain.Tours;

namespace TourCore.Infrastructure.Postgres.Configurations.Tours
{
    public class TourTypeConfiguration : IEntityTypeConfiguration<TourType>
    {
        public void Configure(EntityTypeBuilder<TourType> b)
        {
            b.ToTable("TourTypes");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.HasIndex(x => x.Name).IsUnique();
            b.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> b)
        {
            b.ToTable("Tours");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id)
                .UseIdentityByDefaultColumn()
                .HasIdentityOptions(startValue: 100000000);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.Revision).IsRequired().HasDefaultValue(1L);
            b.Property(x => x.CurrencyId).IsRequired();
            b.Property(x => x.DateMode).IsRequired();
            b.Property(x => x.AvailableDateFrom).HasColumnType("date");
            b.Property(x => x.AvailableDateTo).HasColumnType("date");
            b.Property(x => x.DescriptionUrl).HasMaxLength(2048);
            b.Property(x => x.DescriptionHtml).HasColumnType("text");
            b.Property(x => x.BookingConditionsHtml).HasColumnType("text");
            b.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasMany(x => x.TourTypes)
                .WithMany(x => x.Tours)
                .UsingEntity<Dictionary<string, object>>(
                    "TourTourTypes",
                    right =>
                        right
                            .HasOne<TourType>()
                            .WithMany()
                            .HasForeignKey("TourTypeId")
                            .OnDelete(DeleteBehavior.Cascade),
                    left =>
                        left.HasOne<Tour>()
                            .WithMany()
                            .HasForeignKey("TourId")
                            .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("TourTourTypes");
                        join.HasKey("TourId", "TourTypeId");
                    }
                );
            b.HasMany(x => x.AvailableDates)
                .WithOne(x => x.Tour)
                .HasForeignKey(x => x.TourId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourAvailableDateConfiguration : IEntityTypeConfiguration<TourAvailableDate>
    {
        public void Configure(EntityTypeBuilder<TourAvailableDate> b)
        {
            b.ToTable("TourAvailableDates");
            b.HasKey(x => x.Id);
            b.Property(x => x.Date).HasColumnType("date").IsRequired();
            b.HasIndex(x => new { x.TourId, x.Date }).IsUnique();
        }
    }

    public class TourServiceConfiguration : IEntityTypeConfiguration<TourService>
    {
        public void Configure(EntityTypeBuilder<TourService> b)
        {
            b.ToTable("TourServices");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            b.Property(x => x.OrderIndex).IsRequired();
            b.Property(x => x.IsHidden).IsRequired().HasDefaultValue(false);
            b.Property(x => x.IsRemovable).IsRequired().HasDefaultValue(false);
            b.Property(x => x.IsOptional).IsRequired().HasDefaultValue(false);
            b.Property(x => x.Revision).IsRequired().HasDefaultValue(1L);
            b.HasIndex(x => new { x.TourId, x.OrderIndex });
            b.HasOne(x => x.Tour)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.TourId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.ServiceType)
                .WithMany()
                .HasForeignKey(x => x.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.ParentService)
                .WithMany()
                .HasForeignKey(x => x.ParentServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TourFlightGroupConfiguration : IEntityTypeConfiguration<TourFlightGroup>
    {
        public void Configure(EntityTypeBuilder<TourFlightGroup> b)
        {
            b.ToTable("TourFlightGroups");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Priority).IsRequired();
            b.Property(x => x.OrderIndex).IsRequired();
            b.HasIndex(x => new { x.TourServiceId, x.OrderIndex });
            b.HasOne(x => x.TourService)
                .WithMany(x => x.FlightGroups)
                .HasForeignKey(x => x.TourServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourFlightConfiguration : IEntityTypeConfiguration<TourFlight>
    {
        public void Configure(EntityTypeBuilder<TourFlight> b)
        {
            b.ToTable("TourFlights");
            b.HasKey(x => x.Id);
            b.Property(x => x.OrderIndex).IsRequired();
            b.HasIndex(x => new { x.TourFlightGroupId, x.OrderIndex });
            b.Property(x => x.DepartureCountryIds).HasColumnType("text");
            b.Property(x => x.DepartureCityIds).HasColumnType("text");
            b.Property(x => x.ArrivalCountryIds).HasColumnType("text");
            b.Property(x => x.ArrivalCityIds).HasColumnType("text");
            b.Property(x => x.AirlineIds).HasColumnType("text");
            b.Property(x => x.FlightIds).HasColumnType("text");
            b.Property(x => x.DepartureAirportIds).HasColumnType("text");
            b.Property(x => x.ArrivalAirportIds).HasColumnType("text");
            b.Property(x => x.FareIds).HasColumnType("text");
            b.HasOne(x => x.TourFlightGroup)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.TourFlightGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
