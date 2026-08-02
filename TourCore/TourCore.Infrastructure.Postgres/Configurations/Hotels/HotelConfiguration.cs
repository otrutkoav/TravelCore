using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Hotels
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CountryId)
                .IsRequired();

            builder.Property(x => x.CityId)
                .IsRequired();

            builder.Property(x => x.ResortId)
                .IsRequired(false);

            builder.Property(x => x.CategoryId)
                .IsRequired(false);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(x => x.Stars)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.Address)
                .IsRequired(false)
                .HasMaxLength(254);

            builder.Property(x => x.Phone)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.Fax)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.Website)
                .IsRequired(false)
                .HasMaxLength(254);

            builder.Property(x => x.Latitude)
                .IsRequired(false)
                .HasMaxLength(30);

            builder.Property(x => x.Longitude)
                .IsRequired(false)
                .HasMaxLength(30);

            builder.Property(x => x.IsCruise)
                .IsRequired();

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.Property(x => x.Rank)
                .IsRequired(false);

            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Resort)
                .WithMany()
                .HasForeignKey(x => x.ResortId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}