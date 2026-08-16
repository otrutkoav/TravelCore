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
            b.HasOne(x => x.TourFlightGroup)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.TourFlightGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourFlightRestrictionConfiguration
        : IEntityTypeConfiguration<TourFlightRestriction>
    {
        public void Configure(EntityTypeBuilder<TourFlightRestriction> builder)
        {
            builder.ToTable("TourFlightRestrictions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.ReferenceId).IsRequired();
            builder
                .HasIndex(x => new
                {
                    x.TourFlightId,
                    x.Type,
                    x.ReferenceId,
                })
                .IsUnique();
            builder.HasIndex(x => new { x.Type, x.ReferenceId });
            builder
                .HasOne(x => x.TourFlight)
                .WithMany(x => x.Restrictions)
                .HasForeignKey(x => x.TourFlightId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourHotelGroupConfiguration : IEntityTypeConfiguration<TourHotelGroup>
    {
        public void Configure(EntityTypeBuilder<TourHotelGroup> builder)
        {
            builder.ToTable("TourHotelGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.OrderIndex).IsRequired();
            builder.HasIndex(x => new { x.TourServiceId, x.OrderIndex }).IsUnique();
            builder
                .HasOne(x => x.TourService)
                .WithMany(x => x.HotelGroups)
                .HasForeignKey(x => x.TourServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourHotelConfiguration : IEntityTypeConfiguration<TourHotel>
    {
        public void Configure(EntityTypeBuilder<TourHotel> builder)
        {
            builder.ToTable("TourHotels");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderIndex).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.HasIndex(x => new { x.TourHotelGroupId, x.OrderIndex }).IsUnique();
            builder.HasIndex(x => x.CountryId);
            builder
                .HasOne(x => x.TourHotelGroup)
                .WithMany(x => x.Hotels)
                .HasForeignKey(x => x.TourHotelGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourHotelDurationConfiguration : IEntityTypeConfiguration<TourHotelDuration>
    {
        public void Configure(EntityTypeBuilder<TourHotelDuration> builder)
        {
            builder.ToTable("TourHotelDurations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nights).IsRequired();
            builder.HasIndex(x => new { x.TourHotelId, x.Nights }).IsUnique();
            builder
                .HasOne(x => x.TourHotel)
                .WithMany(x => x.Durations)
                .HasForeignKey(x => x.TourHotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourHotelAllowedHotelConfiguration
        : IEntityTypeConfiguration<TourHotelAllowedHotel>
    {
        public void Configure(EntityTypeBuilder<TourHotelAllowedHotel> builder)
        {
            builder.ToTable("TourHotelAllowedHotels");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.HotelId).IsRequired();
            builder.HasIndex(x => new { x.TourHotelId, x.HotelId }).IsUnique();
            builder.HasIndex(x => x.HotelId);
            builder
                .HasOne(x => x.TourHotel)
                .WithMany(x => x.AllowedHotels)
                .HasForeignKey(x => x.TourHotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourHotelCityConfiguration : IEntityTypeConfiguration<TourHotelCity>
    {
        public void Configure(EntityTypeBuilder<TourHotelCity> builder)
        {
            builder.ToTable("TourHotelCities");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CityId).IsRequired();
            builder.HasIndex(x => new { x.TourHotelId, x.CityId }).IsUnique();
            builder.HasIndex(x => x.CityId);
            builder
                .HasOne(x => x.TourHotel)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.TourHotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferConfiguration : IEntityTypeConfiguration<TourBusTransfer>
    {
        public void Configure(EntityTypeBuilder<TourBusTransfer> builder)
        {
            builder.ToTable("TourBusTransfers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DepartureTimeFrom).HasColumnType("time without time zone");
            builder.Property(x => x.DepartureTimeTo).HasColumnType("time without time zone");
            builder.Property(x => x.ArrivalTimeFrom).HasColumnType("time without time zone");
            builder.Property(x => x.ArrivalTimeTo).HasColumnType("time without time zone");
            builder.HasIndex(x => x.TourServiceId).IsUnique();
            builder
                .HasOne(x => x.TourService)
                .WithMany()
                .HasForeignKey(x => x.TourServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferDepartureCountryConfiguration
        : IEntityTypeConfiguration<TourBusTransferDepartureCountry>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferDepartureCountry> builder)
        {
            builder.ToTable("TourBusTransferDepartureCountries");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.CountryId }).IsUnique();
            builder.HasIndex(x => x.CountryId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.DepartureCountries)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferDepartureCityConfiguration
        : IEntityTypeConfiguration<TourBusTransferDepartureCity>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferDepartureCity> builder)
        {
            builder.ToTable("TourBusTransferDepartureCities");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.CityId }).IsUnique();
            builder.HasIndex(x => x.CityId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.DepartureCities)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferArrivalCountryConfiguration
        : IEntityTypeConfiguration<TourBusTransferArrivalCountry>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferArrivalCountry> builder)
        {
            builder.ToTable("TourBusTransferArrivalCountries");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.CountryId }).IsUnique();
            builder.HasIndex(x => x.CountryId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.ArrivalCountries)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferArrivalCityConfiguration
        : IEntityTypeConfiguration<TourBusTransferArrivalCity>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferArrivalCity> builder)
        {
            builder.ToTable("TourBusTransferArrivalCities");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.CityId }).IsUnique();
            builder.HasIndex(x => x.CityId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.ArrivalCities)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferOptionConfiguration
        : IEntityTypeConfiguration<TourBusTransferOption>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferOption> builder)
        {
            builder.ToTable("TourBusTransferOptions");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.BusTransferId }).IsUnique();
            builder.HasIndex(x => x.BusTransferId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferBoardingPointConfiguration
        : IEntityTypeConfiguration<TourBusTransferBoardingPoint>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferBoardingPoint> builder)
        {
            builder.ToTable("TourBusTransferBoardingPoints");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.BoardingPointId }).IsUnique();
            builder.HasIndex(x => x.BoardingPointId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.BoardingPoints)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourBusTransferTransportConfiguration
        : IEntityTypeConfiguration<TourBusTransferTransport>
    {
        public void Configure(EntityTypeBuilder<TourBusTransferTransport> builder)
        {
            builder.ToTable("TourBusTransferTransports");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.TourBusTransferId, x.TransportId }).IsUnique();
            builder.HasIndex(x => x.TransportId);
            builder
                .HasOne(x => x.TourBusTransfer)
                .WithMany(x => x.Transports)
                .HasForeignKey(x => x.TourBusTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
