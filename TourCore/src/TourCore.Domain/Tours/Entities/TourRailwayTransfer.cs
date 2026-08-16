using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Настройки единственного железнодорожного переезда внутри услуги тура.</summary>
    public class TourRailwayTransfer : AuditableEntity
    {
        public int TourServiceId { get; protected set; }
        public virtual TourService TourService { get; protected set; }
        public TimeSpan DepartureTimeFrom { get; protected set; }
        public TimeSpan DepartureTimeTo { get; protected set; }
        public TimeSpan ArrivalTimeFrom { get; protected set; }
        public TimeSpan ArrivalTimeTo { get; protected set; }
        public virtual ICollection<TourRailwayTransferDepartureCountry> DepartureCountries
        {
            get;
            protected set;
        }
        public virtual ICollection<TourRailwayTransferDepartureCity> DepartureCities
        {
            get;
            protected set;
        }
        public virtual ICollection<TourRailwayTransferArrivalCountry> ArrivalCountries
        {
            get;
            protected set;
        }
        public virtual ICollection<TourRailwayTransferArrivalCity> ArrivalCities
        {
            get;
            protected set;
        }
        public virtual ICollection<TourRailwayTransferOption> Options { get; protected set; }
        public virtual ICollection<TourRailwayTransferTransport> Transports { get; protected set; }

        protected TourRailwayTransfer()
        {
            DepartureCountries = new List<TourRailwayTransferDepartureCountry>();
            DepartureCities = new List<TourRailwayTransferDepartureCity>();
            ArrivalCountries = new List<TourRailwayTransferArrivalCountry>();
            ArrivalCities = new List<TourRailwayTransferArrivalCity>();
            Options = new List<TourRailwayTransferOption>();
            Transports = new List<TourRailwayTransferTransport>();
        }

        public TourRailwayTransfer(
            int tourServiceId,
            TimeSpan departureTimeFrom,
            TimeSpan departureTimeTo,
            TimeSpan arrivalTimeFrom,
            TimeSpan arrivalTimeTo,
            DateTime createdAt
        )
            : this()
        {
            if (tourServiceId <= 0)
                throw new DomainException("Tour service is required.");
            TourServiceId = tourServiceId;
            SetTimeRanges(departureTimeFrom, departureTimeTo, arrivalTimeFrom, arrivalTimeTo);
            SetCreated(createdAt);
        }

        public void UpdateTimeRanges(
            TimeSpan departureTimeFrom,
            TimeSpan departureTimeTo,
            TimeSpan arrivalTimeFrom,
            TimeSpan arrivalTimeTo,
            DateTime updatedAt
        )
        {
            SetTimeRanges(departureTimeFrom, departureTimeTo, arrivalTimeFrom, arrivalTimeTo);
            SetUpdated(updatedAt);
        }

        public void ReplaceDepartureCountries(IEnumerable<int> ids)
        {
            DepartureCountries.Clear();
            foreach (var id in Distinct(ids))
                DepartureCountries.Add(new TourRailwayTransferDepartureCountry(id));
        }

        public void ReplaceDepartureCities(IEnumerable<int> ids)
        {
            DepartureCities.Clear();
            foreach (var id in Distinct(ids))
                DepartureCities.Add(new TourRailwayTransferDepartureCity(id));
        }

        public void ReplaceArrivalCountries(IEnumerable<int> ids)
        {
            ArrivalCountries.Clear();
            foreach (var id in Distinct(ids))
                ArrivalCountries.Add(new TourRailwayTransferArrivalCountry(id));
        }

        public void ReplaceArrivalCities(IEnumerable<int> ids)
        {
            ArrivalCities.Clear();
            foreach (var id in Distinct(ids))
                ArrivalCities.Add(new TourRailwayTransferArrivalCity(id));
        }

        public void ReplaceOptions(IEnumerable<int> ids)
        {
            Options.Clear();
            foreach (var id in Distinct(ids))
                Options.Add(new TourRailwayTransferOption(id));
        }

        public void ReplaceTransports(IEnumerable<int> ids)
        {
            Transports.Clear();
            foreach (var id in Distinct(ids))
                Transports.Add(new TourRailwayTransferTransport(id));
        }

        private static IEnumerable<int> Distinct(IEnumerable<int> ids) =>
            (ids ?? Enumerable.Empty<int>()).Distinct();

        private void SetTimeRanges(
            TimeSpan departureTimeFrom,
            TimeSpan departureTimeTo,
            TimeSpan arrivalTimeFrom,
            TimeSpan arrivalTimeTo
        )
        {
            ValidateTimeRange(departureTimeFrom, departureTimeTo, "Departure");
            ValidateTimeRange(arrivalTimeFrom, arrivalTimeTo, "Arrival");
            DepartureTimeFrom = departureTimeFrom;
            DepartureTimeTo = departureTimeTo;
            ArrivalTimeFrom = arrivalTimeFrom;
            ArrivalTimeTo = arrivalTimeTo;
        }

        private static void ValidateTimeRange(TimeSpan from, TimeSpan to, string name)
        {
            if (from < TimeSpan.Zero || from >= TimeSpan.FromDays(1))
                throw new DomainException($"{name} time from must be within one day.");
            if (to < TimeSpan.Zero || to >= TimeSpan.FromDays(1))
                throw new DomainException($"{name} time to must be within one day.");
            if (from > to)
                throw new DomainException($"{name} time from cannot exceed time to.");
        }
    }
}
