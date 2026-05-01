using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Hotels
{
    public class HotelCategoryConfiguration : EntityTypeConfiguration<HotelCategory>
    {
        public HotelCategoryConfiguration()
        {
            ToTable("HotelCategories");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(50);

            Property(x => x.PrintOrder)
                .IsOptional();

            Property(x => x.GlobalCode)
                .IsOptional()
                .HasMaxLength(20);

            Property(x => x.Description)
                .IsOptional();
        }
    }
}