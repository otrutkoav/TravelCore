using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Bus
{
    public class BusScheduleConfiguration : IEntityTypeConfiguration<BusSchedule>
    {
        public void Configure(EntityTypeBuilder<BusSchedule> builder)
        {
            builder.ToTable("BusSchedules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BusTransferId)
                .IsRequired();

            builder.Property(x => x.DateFrom)
                .IsRequired(false);

            builder.Property(x => x.DateTo)
                .IsRequired(false);

            builder.Property(x => x.TimeFrom)
                .IsRequired(false);

            builder.Property(x => x.TimeTo)
                .IsRequired(false);

            builder.Property(x => x.DaysOfWeekValue)
             .HasColumnName("DaysOfWeek")
             .IsRequired(false)
             .HasMaxLength(7);

            builder.Property(x => x.DaysOnRoad)
                .IsRequired(false);

            builder.Ignore(x => x.DaysOfWeek);

            builder.HasOne(x => x.BusTransfer)
                .WithMany()
                .HasForeignKey(x => x.BusTransferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}