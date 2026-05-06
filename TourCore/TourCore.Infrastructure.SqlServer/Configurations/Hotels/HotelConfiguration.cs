using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Hotels
{
    public class HotelConfiguration : EntityTypeConfiguration<Hotel>
    {
        public HotelConfiguration()
        {
            ToTable("Hotels");

            HasKey(x => x.Id);

            Property(x => x.CountryId)
                .IsRequired();

            Property(x => x.CityId)
                .IsRequired();

            Property(x => x.ResortId)
                .IsOptional();

            Property(x => x.CategoryId)
                .IsOptional();

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(200);

            Property(x => x.Stars)
                .IsRequired()
                .HasMaxLength(20);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            Property(x => x.Address)
                .IsOptional()
                .HasMaxLength(254);

            Property(x => x.Phone)
                .IsOptional()
                .HasMaxLength(50);

            Property(x => x.Fax)
                .IsOptional()
                .HasMaxLength(20);

            Property(x => x.Email)
                .IsOptional()
                .HasMaxLength(50);

            Property(x => x.Website)
                .IsOptional()
                .HasMaxLength(254);

            Property(x => x.Latitude)
                .IsOptional()
                .HasMaxLength(30);

            Property(x => x.Longitude)
                .IsOptional()
                .HasMaxLength(30);

            Property(x => x.IsCruise)
                .IsRequired();

            Property(x => x.SortOrder)
                .IsRequired();

            Property(x => x.Rank)
                .IsOptional();

            HasRequired(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Resort)
                .WithMany()
                .HasForeignKey(x => x.ResortId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}