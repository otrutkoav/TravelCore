using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations
{
    public class MealTypeConfiguration : EntityTypeConfiguration<MealType>
    {
        public MealTypeConfiguration()
        {
            ToTable("MealTypes");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(30);

            Property(x => x.GlobalCode)
                .IsOptional()
                .HasMaxLength(10);

            Property(x => x.SortOrder)
                .IsRequired();

            Property(x => x.Description)
                .IsOptional();
        }
    }
}