using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Tours;

namespace TourCore.Infrastructure.SqlServer.Configurations.Tours
{
    internal static class RailwayIndex
    {
        public static IndexAnnotation Unique(string name, int order) =>
            new IndexAnnotation(new IndexAttribute(name, order) { IsUnique = true });

        public static IndexAnnotation UniqueAndLookup(string unique, int order, string lookup) =>
            new IndexAnnotation(
                new[]
                {
                    new IndexAttribute(unique, order) { IsUnique = true },
                    new IndexAttribute(lookup),
                }
            );
    }

    public class TourRailwayTransferConfiguration : EntityTypeConfiguration<TourRailwayTransfer>
    {
        public TourRailwayTransferConfiguration()
        {
            ToTable("TourRailwayTransfers");
            HasKey(x => x.Id);
            Property(x => x.DepartureTimeFrom).HasColumnType("time");
            Property(x => x.DepartureTimeTo).HasColumnType("time");
            Property(x => x.ArrivalTimeFrom).HasColumnType("time");
            Property(x => x.ArrivalTimeTo).HasColumnType("time");
            Property(x => x.TourServiceId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourRailwayTransfers_TourService")
                        {
                            IsUnique = true,
                        }
                    )
                );
            HasRequired(x => x.TourService)
                .WithMany()
                .HasForeignKey(x => x.TourServiceId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourRailwayTransferDepartureCountryConfiguration
        : EntityTypeConfiguration<TourRailwayTransferDepartureCountry>
    {
        public TourRailwayTransferDepartureCountryConfiguration()
        {
            const string u = "UX_TourRailwayTransferDepartureCountries_Transfer_Country";
            ToTable("TourRailwayTransferDepartureCountries");
            HasKey(x => x.Id);
            Property(x => x.TourRailwayTransferId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, RailwayIndex.Unique(u, 0));
            Property(x => x.CountryId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    RailwayIndex.UniqueAndLookup(
                        u,
                        1,
                        "IX_TourRailwayTransferDepartureCountries_Country"
                    )
                );
            HasRequired(x => x.TourRailwayTransfer)
                .WithMany(x => x.DepartureCountries)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourRailwayTransferDepartureCityConfiguration
        : EntityTypeConfiguration<TourRailwayTransferDepartureCity>
    {
        public TourRailwayTransferDepartureCityConfiguration()
        {
            const string u = "UX_TourRailwayTransferDepartureCities_Transfer_City";
            ToTable("TourRailwayTransferDepartureCities");
            HasKey(x => x.Id);
            Property(x => x.TourRailwayTransferId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, RailwayIndex.Unique(u, 0));
            Property(x => x.CityId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    RailwayIndex.UniqueAndLookup(u, 1, "IX_TourRailwayTransferDepartureCities_City")
                );
            HasRequired(x => x.TourRailwayTransfer)
                .WithMany(x => x.DepartureCities)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourRailwayTransferArrivalCountryConfiguration
        : EntityTypeConfiguration<TourRailwayTransferArrivalCountry>
    {
        public TourRailwayTransferArrivalCountryConfiguration()
        {
            const string u = "UX_TourRailwayTransferArrivalCountries_Transfer_Country";
            ToTable("TourRailwayTransferArrivalCountries");
            HasKey(x => x.Id);
            Property(x => x.TourRailwayTransferId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, RailwayIndex.Unique(u, 0));
            Property(x => x.CountryId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    RailwayIndex.UniqueAndLookup(
                        u,
                        1,
                        "IX_TourRailwayTransferArrivalCountries_Country"
                    )
                );
            HasRequired(x => x.TourRailwayTransfer)
                .WithMany(x => x.ArrivalCountries)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourRailwayTransferArrivalCityConfiguration
        : EntityTypeConfiguration<TourRailwayTransferArrivalCity>
    {
        public TourRailwayTransferArrivalCityConfiguration()
        {
            const string u = "UX_TourRailwayTransferArrivalCities_Transfer_City";
            ToTable("TourRailwayTransferArrivalCities");
            HasKey(x => x.Id);
            Property(x => x.TourRailwayTransferId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, RailwayIndex.Unique(u, 0));
            Property(x => x.CityId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    RailwayIndex.UniqueAndLookup(u, 1, "IX_TourRailwayTransferArrivalCities_City")
                );
            HasRequired(x => x.TourRailwayTransfer)
                .WithMany(x => x.ArrivalCities)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourRailwayTransferOptionConfiguration
        : EntityTypeConfiguration<TourRailwayTransferOption>
    {
        public TourRailwayTransferOptionConfiguration()
        {
            const string u = "UX_TourRailwayTransferOptions_Transfer_Option";
            ToTable("TourRailwayTransferOptions");
            HasKey(x => x.Id);
            Property(x => x.TourRailwayTransferId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, RailwayIndex.Unique(u, 0));
            Property(x => x.RailwayTransferId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    RailwayIndex.UniqueAndLookup(
                        u,
                        1,
                        "IX_TourRailwayTransferOptions_RailwayTransfer"
                    )
                );
            HasRequired(x => x.TourRailwayTransfer)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourRailwayTransferTransportConfiguration
        : EntityTypeConfiguration<TourRailwayTransferTransport>
    {
        public TourRailwayTransferTransportConfiguration()
        {
            const string u = "UX_TourRailwayTransferTransports_Transfer_Transport";
            ToTable("TourRailwayTransferTransports");
            HasKey(x => x.Id);
            Property(x => x.TourRailwayTransferId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, RailwayIndex.Unique(u, 0));
            Property(x => x.TransportId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    RailwayIndex.UniqueAndLookup(u, 1, "IX_TourRailwayTransferTransports_Transport")
                );
            HasRequired(x => x.TourRailwayTransfer)
                .WithMany(x => x.Transports)
                .HasForeignKey(x => x.TourRailwayTransferId)
                .WillCascadeOnDelete(true);
        }
    }
}
