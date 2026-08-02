using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Avia
{
    public class CharterSeasonConfiguration : IEntityTypeConfiguration<CharterSeason>
    {
        public void Configure(EntityTypeBuilder<CharterSeason> builder)
        {
            builder.ToTable("CharterSeasons");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CharterId)
                .IsRequired();

            builder.Property(x => x.DateFrom)
                .IsRequired(false);

            builder.Property(x => x.DateTo)
                .IsRequired(false);

            builder.Property(x => x.TimeFrom)
                .IsRequired(false);

            builder.Property(x => x.TimeTo)
                .IsRequired(false);

            builder.Property(x => x.IsNextDayArrival)
                .IsRequired();

            builder.Property(x => x.Remark)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(x => x.DaysOfWeekValue)
             .HasColumnName("DaysOfWeek")
             .IsRequired(false)
             .HasMaxLength(7);

            builder.Ignore(x => x.DaysOfWeek);
        }
    }
}