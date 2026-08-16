using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Tours;

namespace TourCore.Infrastructure.Postgres.Configurations.Tours
{
    public class TourRailwayTransferConfiguration : IEntityTypeConfiguration<TourRailwayTransfer>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransfer> b)
        {
            b.ToTable("TourRailwayTransfers");
            b.HasKey(x => x.Id);
            b.Property(x => x.DepartureTimeFrom).HasColumnType("time without time zone");
            b.Property(x => x.DepartureTimeTo).HasColumnType("time without time zone");
            b.Property(x => x.ArrivalTimeFrom).HasColumnType("time without time zone");
            b.Property(x => x.ArrivalTimeTo).HasColumnType("time without time zone");
            b.HasIndex(x => x.TourServiceId).IsUnique();
            b.HasOne(x => x.TourService)
                .WithMany()
                .HasForeignKey(x => x.TourServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourRailwayTransferDepartureCountryConfiguration
        : IEntityTypeConfiguration<TourRailwayTransferDepartureCountry>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransferDepartureCountry> b)
        {
            b.ToTable("TourRailwayTransferDepartureCountries");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TourRailwayTransferId, x.CountryId }).IsUnique();
            b.HasIndex(x => x.CountryId);
            b.HasOne(x => x.TourRailwayTransfer)
                .WithMany(x => x.DepartureCountries)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourRailwayTransferDepartureCityConfiguration
        : IEntityTypeConfiguration<TourRailwayTransferDepartureCity>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransferDepartureCity> b)
        {
            b.ToTable("TourRailwayTransferDepartureCities");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TourRailwayTransferId, x.CityId }).IsUnique();
            b.HasIndex(x => x.CityId);
            b.HasOne(x => x.TourRailwayTransfer)
                .WithMany(x => x.DepartureCities)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourRailwayTransferArrivalCountryConfiguration
        : IEntityTypeConfiguration<TourRailwayTransferArrivalCountry>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransferArrivalCountry> b)
        {
            b.ToTable("TourRailwayTransferArrivalCountries");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TourRailwayTransferId, x.CountryId }).IsUnique();
            b.HasIndex(x => x.CountryId);
            b.HasOne(x => x.TourRailwayTransfer)
                .WithMany(x => x.ArrivalCountries)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourRailwayTransferArrivalCityConfiguration
        : IEntityTypeConfiguration<TourRailwayTransferArrivalCity>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransferArrivalCity> b)
        {
            b.ToTable("TourRailwayTransferArrivalCities");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TourRailwayTransferId, x.CityId }).IsUnique();
            b.HasIndex(x => x.CityId);
            b.HasOne(x => x.TourRailwayTransfer)
                .WithMany(x => x.ArrivalCities)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourRailwayTransferOptionConfiguration
        : IEntityTypeConfiguration<TourRailwayTransferOption>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransferOption> b)
        {
            b.ToTable("TourRailwayTransferOptions");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TourRailwayTransferId, x.RailwayTransferId }).IsUnique();
            b.HasIndex(x => x.RailwayTransferId);
            b.HasOne(x => x.TourRailwayTransfer)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TourRailwayTransferTransportConfiguration
        : IEntityTypeConfiguration<TourRailwayTransferTransport>
    {
        public void Configure(EntityTypeBuilder<TourRailwayTransferTransport> b)
        {
            b.ToTable("TourRailwayTransferTransports");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TourRailwayTransferId, x.TransportId }).IsUnique();
            b.HasIndex(x => x.TransportId);
            b.HasOne(x => x.TourRailwayTransfer)
                .WithMany(x => x.Transports)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
