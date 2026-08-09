using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Tours;

namespace TourCore.Infrastructure.SqlServer.Configurations.Tours
{
    public class TourTypeConfiguration : EntityTypeConfiguration<TourType>
    {
        public TourTypeConfiguration()
        {
            ToTable("TourTypes");
            HasKey(x => x.Id);
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UX_TourTypes_Name") { IsUnique = true })
                );
            HasOptional(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .WillCascadeOnDelete(false);
            HasMany(x => x.Tours)
                .WithMany(x => x.TourTypes)
                .Map(m =>
                {
                    m.ToTable("TourTourTypes");
                    m.MapLeftKey("TourTypeId");
                    m.MapRightKey("TourId");
                });
        }
    }

    public class TourConfiguration : EntityTypeConfiguration<Tour>
    {
        public TourConfiguration()
        {
            ToTable("Tours");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(128);
            Property(x => x.Status).IsRequired();
            Property(x => x.Revision).IsRequired();
            Property(x => x.CurrencyId).IsRequired();
            Property(x => x.DateMode).IsRequired();
            Property(x => x.AvailableDateFrom).HasColumnType("date");
            Property(x => x.AvailableDateTo).HasColumnType("date");
            Property(x => x.DescriptionUrl).IsOptional().HasMaxLength(2048);
            Property(x => x.DescriptionHtml).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.BookingConditionsHtml).IsOptional().HasColumnType("nvarchar(max)");
            HasRequired(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .WillCascadeOnDelete(false);
            HasMany(x => x.AvailableDates)
                .WithRequired(x => x.Tour)
                .HasForeignKey(x => x.TourId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourAvailableDateConfiguration : EntityTypeConfiguration<TourAvailableDate>
    {
        public TourAvailableDateConfiguration()
        {
            ToTable("TourAvailableDates");
            HasKey(x => x.Id);
            Property(x => x.Date)
                .HasColumnType("date")
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourAvailableDates_Tour_Date", 1) { IsUnique = true }
                    )
                );
            Property(x => x.TourId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourAvailableDates_Tour_Date", 0) { IsUnique = true }
                    )
                );
        }
    }

    public class TourServiceConfiguration : EntityTypeConfiguration<TourService>
    {
        public TourServiceConfiguration()
        {
            ToTable("TourServices");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(256);
            Property(x => x.OrderIndex)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_TourServices_Tour_Order", 1))
                );
            Property(x => x.TourId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_TourServices_Tour_Order", 0))
                );
            Property(x => x.IsHidden).IsRequired();
            Property(x => x.IsRemovable).IsRequired();
            Property(x => x.IsOptional).IsRequired();
            Property(x => x.Revision).IsRequired();
            HasRequired(x => x.Tour)
                .WithMany(x => x.Services)
                .HasForeignKey(x => x.TourId)
                .WillCascadeOnDelete(true);
            HasRequired(x => x.ServiceType)
                .WithMany()
                .HasForeignKey(x => x.ServiceTypeId)
                .WillCascadeOnDelete(false);
            HasOptional(x => x.ParentService)
                .WithMany()
                .HasForeignKey(x => x.ParentServiceId)
                .WillCascadeOnDelete(false);
        }
    }

    public class TourFlightGroupConfiguration : EntityTypeConfiguration<TourFlightGroup>
    {
        public TourFlightGroupConfiguration()
        {
            ToTable("TourFlightGroups");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(128);
            Property(x => x.Priority).IsRequired();
            Property(x => x.OrderIndex).IsRequired();
            HasRequired(x => x.TourService)
                .WithMany(x => x.FlightGroups)
                .HasForeignKey(x => x.TourServiceId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourFlightConfiguration : EntityTypeConfiguration<TourFlight>
    {
        public TourFlightConfiguration()
        {
            ToTable("TourFlights");
            HasKey(x => x.Id);
            Property(x => x.OrderIndex).IsRequired();
            Property(x => x.DepartureCountryIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.DepartureCityIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.ArrivalCountryIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.ArrivalCityIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.AirlineIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.FlightIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.DepartureAirportIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.ArrivalAirportIds).IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.FareIds).IsOptional().HasColumnType("nvarchar(max)");
            HasRequired(x => x.TourFlightGroup)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.TourFlightGroupId)
                .WillCascadeOnDelete(true);
        }
    }
}
