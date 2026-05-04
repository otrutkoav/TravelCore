using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Avia
{
    public class CharterSeasonConfiguration : EntityTypeConfiguration<CharterSeason>
    {
        public CharterSeasonConfiguration()
        {
            ToTable("CharterSeasons");

            HasKey(x => x.Id);

            Property(x => x.CharterId)
                .IsRequired();

            Property(x => x.DateFrom)
                .IsOptional();

            Property(x => x.DateTo)
                .IsOptional();

            Property(x => x.TimeFrom)
                .IsOptional();

            Property(x => x.TimeTo)
                .IsOptional();

            Property(x => x.IsNextDayArrival)
                .IsRequired();

            Property(x => x.Remark)
                .IsOptional()
                .HasMaxLength(20);

            Property(x => x.DaysOfWeekValue)
             .HasColumnName("DaysOfWeek")
             .IsOptional()
             .HasMaxLength(7);

            Ignore(x => x.DaysOfWeek);
        }
    }
}