using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Hotels
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.Places)
                .IsRequired(false);

            builder.Property(x => x.ExtraPlaces)
                .IsRequired(false);

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired(false);
        }
    }
}