using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Domain.Bus.Entities
{
    /// <summary>
    /// Расписание автобусного переезда.
    /// Определяет период действия, время и дни недели.
    /// </summary>
    public class BusSchedule : AuditableEntity
    {
        public int BusTransferId { get; protected set; }
        public virtual BusTransfer BusTransfer { get; protected set; }

        public DateTime? DateFrom { get; protected set; }
        public DateTime? DateTo { get; protected set; }

        public DateTime? TimeFrom { get; protected set; }
        public DateTime? TimeTo { get; protected set; }

        public DaysOfWeek DaysOfWeek { get; protected set; }

        public short? DaysOnRoad { get; protected set; }

        protected BusSchedule()
        {
        }

        public BusSchedule(
            int busTransferId,
            DateTime createdAt,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            DaysOfWeek daysOfWeek = null,
            short? daysOnRoad = null)
        {
            SetBusTransferId(busTransferId);
            SetDateRange(dateFrom, dateTo);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDaysOfWeek(daysOfWeek);
            SetDaysOnRoad(daysOnRoad);

            SetCreated(createdAt);
        }

        public void Update(
            int busTransferId,
            DateTime updatedAt,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            DaysOfWeek daysOfWeek = null,
            short? daysOnRoad = null)
        {
            SetBusTransferId(busTransferId);
            SetDateRange(dateFrom, dateTo);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDaysOfWeek(daysOfWeek);
            SetDaysOnRoad(daysOnRoad);

            SetUpdated(updatedAt);
        }

        private void SetBusTransferId(int value)
        {
            if (value <= 0)
                throw new DomainException("Bus schedule bus transfer id must be greater than zero.");

            BusTransferId = value;
        }

        private void SetDateRange(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom.HasValue && dateTo.HasValue && dateFrom.Value.Date > dateTo.Value.Date)
                throw new DomainException("Bus schedule start date cannot be greater than end date.");

            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        private void SetTimeFrom(DateTime? value)
        {
            TimeFrom = value;
        }

        private void SetTimeTo(DateTime? value)
        {
            TimeTo = value;
        }

        private void SetDaysOfWeek(DaysOfWeek value)
        {
            DaysOfWeek = value;
        }

        private void SetDaysOnRoad(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Bus schedule days on road cannot be negative.");

            DaysOnRoad = value;
        }
    }
}