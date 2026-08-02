using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Finance
{
    public class RealCourseConfiguration : IEntityTypeConfiguration<RealCourse>
    {
        public void Configure(EntityTypeBuilder<RealCourse> builder)
        {
            builder.ToTable("RealCourses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FromRateCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.ToRateCode)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.Course)
                .IsRequired(false)
                .HasPrecision(18, 6);

            builder.Property(x => x.CentralBankCourse)
                .IsRequired(false)
                .HasPrecision(18, 6);

            builder.Property(x => x.DateBeg)
                .IsRequired(false);

            builder.Property(x => x.DateEnd)
                .IsRequired(false);

            builder.Ignore(x => x.FromRate);
            builder.Ignore(x => x.ToRate);
        }
    }
}