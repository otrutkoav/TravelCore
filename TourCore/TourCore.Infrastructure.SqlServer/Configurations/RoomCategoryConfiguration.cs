using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations
{
    public class RoomCategoryConfiguration : EntityTypeConfiguration<RoomCategory>
    {
        public RoomCategoryConfiguration()
        {
            ToTable("RoomCategories");

            HasKey(x => x.Id);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(40);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.SortOrder)
                .IsRequired();

            Property(x => x.Description)
                .IsOptional();
        }
    }
}