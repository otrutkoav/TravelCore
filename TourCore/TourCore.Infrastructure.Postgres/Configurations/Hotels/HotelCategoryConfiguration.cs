using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Hotels
{
    public class HotelCategoryConfiguration : IEntityTypeConfiguration<HotelCategory>
    {
        public void Configure(EntityTypeBuilder<HotelCategory> builder)
        {
            builder.ToTable("HotelCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.PrintOrder)
                .IsRequired(false);

            builder.Property(x => x.GlobalCode)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(x => x.Description)
                .IsRequired(false);
        }
    }
}