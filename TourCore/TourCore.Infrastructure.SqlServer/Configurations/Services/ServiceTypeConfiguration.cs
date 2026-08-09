using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Services.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Services
{
    public class ServiceTypeConfiguration : EntityTypeConfiguration<ServiceType>
    {
        public ServiceTypeConfiguration()
        {
            ToTable("ServiceTypes");
            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.NameEn).IsOptional().HasMaxLength(50);
            Property(x => x.Code)
                .IsOptional()
                .HasMaxLength(10)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UX_ServiceTypes_Code") { IsUnique = true }));
            Property(x => x.IsDuration).IsRequired();
            Property(x => x.IsRoute).IsRequired();
        }
    }
}
