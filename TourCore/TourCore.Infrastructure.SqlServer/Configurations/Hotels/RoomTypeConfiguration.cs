using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Hotels
{
    public class RoomTypeConfiguration : EntityTypeConfiguration<RoomType>
    {
        public RoomTypeConfiguration()
        {
            ToTable("RoomTypes");

            HasKey(x => x.Id);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(25);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.Places)
                .IsOptional();

            Property(x => x.ExtraPlaces)
                .IsOptional();

            Property(x => x.SortOrder)
                .IsRequired();

            Property(x => x.Description)
                .IsOptional();
        }
    }
}