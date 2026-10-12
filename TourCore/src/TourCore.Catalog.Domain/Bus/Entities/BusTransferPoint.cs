using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Bus.Entities
{
    /// <summary>
    /// Точка или участок автобусного переезда.
    /// </summary>
    public class BusTransferPoint : AuditableEntity
    {
        public int BusTransferId { get; protected set; }
        public virtual BusTransfer BusTransfer { get; protected set; }

        public int CountryFromId { get; protected set; }
        public virtual Country CountryFrom { get; protected set; }

        public int CityFromId { get; protected set; }
        public virtual City CityFrom { get; protected set; }

        public int CountryToId { get; protected set; }
        public virtual Country CountryTo { get; protected set; }

        public int CityToId { get; protected set; }
        public virtual City CityTo { get; protected set; }

        public DateTime? TimeFrom { get; protected set; }
        public DateTime? TimeTo { get; protected set; }

        public short? DayFrom { get; protected set; }
        public short? DayTo { get; protected set; }

        protected BusTransferPoint()
        {
        }

        public BusTransferPoint(
            int busTransferId,
            int countryFromId,
            int cityFromId,
            int countryToId,
            int cityToId,
            DateTime createdAt,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            short? dayFrom = null,
            short? dayTo = null)
        {
            SetBusTransferId(busTransferId);
            SetCountryFromId(countryFromId);
            SetCityFromId(cityFromId);
            SetCountryToId(countryToId);
            SetCityToId(cityToId);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDayFrom(dayFrom);
            SetDayTo(dayTo);

            SetCreated(createdAt);
        }

        public void Update(
            int busTransferId,
            int countryFromId,
            int cityFromId,
            int countryToId,
            int cityToId,
            DateTime updatedAt,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            short? dayFrom = null,
            short? dayTo = null)
        {
            SetBusTransferId(busTransferId);
            SetCountryFromId(countryFromId);
            SetCityFromId(cityFromId);
            SetCountryToId(countryToId);
            SetCityToId(cityToId);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDayFrom(dayFrom);
            SetDayTo(dayTo);

            SetUpdated(updatedAt);
        }

        private void SetBusTransferId(int value)
        {
            if (value <= 0)
                throw new DomainException("Bus transfer point bus transfer id must be greater than zero.");

            BusTransferId = value;
        }

        private void SetCountryFromId(int value)
        {
            if (value <= 0)
                throw new DomainException("Bus transfer point departure country id must be greater than zero.");

            CountryFromId = value;
        }

        private void SetCityFromId(int value)
        {
            if (value <= 0)
                throw new DomainException("Bus transfer point departure city id must be greater than zero.");

            CityFromId = value;
        }

        private void SetCountryToId(int value)
        {
            if (value <= 0)
                throw new DomainException("Bus transfer point arrival country id must be greater than zero.");

            CountryToId = value;
        }

        private void SetCityToId(int value)
        {
            if (value <= 0)
                throw new DomainException("Bus transfer point arrival city id must be greater than zero.");

            CityToId = value;
        }

        private void SetTimeFrom(DateTime? value)
        {
            TimeFrom = value;
        }

        private void SetTimeTo(DateTime? value)
        {
            TimeTo = value;
        }

        private void SetDayFrom(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Bus transfer point departure day cannot be negative.");

            DayFrom = value;
        }

        private void SetDayTo(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Bus transfer point arrival day cannot be negative.");

            DayTo = value;
        }
    }
}