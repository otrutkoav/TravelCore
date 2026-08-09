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
            HasRequired(x => x.TourFlightGroup)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.TourFlightGroupId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourFlightRestrictionConfiguration : EntityTypeConfiguration<TourFlightRestriction>
    {
        public TourFlightRestrictionConfiguration()
        {
            ToTable("TourFlightRestrictions");
            HasKey(x => x.Id);
            Property(x => x.Type).IsRequired();
            Property(x => x.ReferenceId).IsRequired();
            HasRequired(x => x.TourFlight)
                .WithMany(x => x.Restrictions)
                .HasForeignKey(x => x.TourFlightId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourHotelGroupConfiguration : EntityTypeConfiguration<TourHotelGroup>
    {
        public TourHotelGroupConfiguration()
        {
            ToTable("TourHotelGroups");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(128);
            Property(x => x.Priority).IsRequired();
            Property(x => x.OrderIndex)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotelGroups_Service_Order", 1)
                        {
                            IsUnique = true,
                        }
                    )
                );
            Property(x => x.TourServiceId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotelGroups_Service_Order", 0)
                        {
                            IsUnique = true,
                        }
                    )
                );
            HasRequired(x => x.TourService)
                .WithMany(x => x.HotelGroups)
                .HasForeignKey(x => x.TourServiceId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourHotelConfiguration : EntityTypeConfiguration<TourHotel>
    {
        public TourHotelConfiguration()
        {
            ToTable("TourHotels");
            HasKey(x => x.Id);
            Property(x => x.OrderIndex)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotels_Group_Order", 1) { IsUnique = true }
                    )
                );
            Property(x => x.TourHotelGroupId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotels_Group_Order", 0) { IsUnique = true }
                    )
                );
            Property(x => x.CountryId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_TourHotels_Country"))
                );
            HasRequired(x => x.TourHotelGroup)
                .WithMany(x => x.Hotels)
                .HasForeignKey(x => x.TourHotelGroupId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourHotelDurationConfiguration : EntityTypeConfiguration<TourHotelDuration>
    {
        public TourHotelDurationConfiguration()
        {
            ToTable("TourHotelDurations");
            HasKey(x => x.Id);
            Property(x => x.Nights)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotelDurations_Hotel_Nights", 1)
                        {
                            IsUnique = true,
                        }
                    )
                );
            Property(x => x.TourHotelId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotelDurations_Hotel_Nights", 0)
                        {
                            IsUnique = true,
                        }
                    )
                );
            HasRequired(x => x.TourHotel)
                .WithMany(x => x.Durations)
                .HasForeignKey(x => x.TourHotelId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourHotelAllowedHotelConfiguration : EntityTypeConfiguration<TourHotelAllowedHotel>
    {
        public TourHotelAllowedHotelConfiguration()
        {
            ToTable("TourHotelAllowedHotels");
            HasKey(x => x.Id);
            Property(x => x.HotelId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new[]
                        {
                            new IndexAttribute("UX_TourHotelAllowedHotels_TourHotel_Hotel", 1)
                            {
                                IsUnique = true,
                            },
                            new IndexAttribute("IX_TourHotelAllowedHotels_Hotel"),
                        }
                    )
                );
            Property(x => x.TourHotelId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotelAllowedHotels_TourHotel_Hotel", 0)
                        {
                            IsUnique = true,
                        }
                    )
                );
            HasRequired(x => x.TourHotel)
                .WithMany(x => x.AllowedHotels)
                .HasForeignKey(x => x.TourHotelId)
                .WillCascadeOnDelete(true);
        }
    }

    public class TourHotelCityConfiguration : EntityTypeConfiguration<TourHotelCity>
    {
        public TourHotelCityConfiguration()
        {
            ToTable("TourHotelCities");
            HasKey(x => x.Id);
            Property(x => x.CityId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new[]
                        {
                            new IndexAttribute("UX_TourHotelCities_TourHotel_City", 1)
                            {
                                IsUnique = true,
                            },
                            new IndexAttribute("IX_TourHotelCities_City"),
                        }
                    )
                );
            Property(x => x.TourHotelId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("UX_TourHotelCities_TourHotel_City", 0)
                        {
                            IsUnique = true,
                        }
                    )
                );
            HasRequired(x => x.TourHotel)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.TourHotelId)
                .WillCascadeOnDelete(true);
        }
    }
}
