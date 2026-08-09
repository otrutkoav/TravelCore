using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Services.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Services
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.NameEn).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Code).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.IsDuration).IsRequired();
            builder.Property(x => x.IsRoute).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
