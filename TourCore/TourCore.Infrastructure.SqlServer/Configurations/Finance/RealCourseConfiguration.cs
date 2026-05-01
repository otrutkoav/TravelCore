using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Finance
{
    public class RealCourseConfiguration : EntityTypeConfiguration<RealCourse>
    {
        public RealCourseConfiguration()
        {
            ToTable("RealCourses");

            HasKey(x => x.Id);

            Property(x => x.FromRateCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.ToRateCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.Course)
                .IsOptional()
                .HasPrecision(18, 6);

            Property(x => x.CentralBankCourse)
                .IsOptional()
                .HasPrecision(18, 6);

            Property(x => x.DateBeg)
                .IsOptional();

            Property(x => x.DateEnd)
                .IsOptional();

            Ignore(x => x.FromRate);
            Ignore(x => x.ToRate);
        }
    }
}