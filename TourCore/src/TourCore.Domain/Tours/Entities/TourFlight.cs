using System;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourFlight : AuditableEntity
    {
        public int TourFlightGroupId { get; protected set; }
        public virtual TourFlightGroup TourFlightGroup { get; protected set; }
        public int OrderIndex { get; protected set; }
        public string DepartureCountryIds { get; protected set; }
        public string DepartureCityIds { get; protected set; }
        public string ArrivalCountryIds { get; protected set; }
        public string ArrivalCityIds { get; protected set; }
        public string AirlineIds { get; protected set; }
        public string FlightIds { get; protected set; }
        public string DepartureAirportIds { get; protected set; }
        public string ArrivalAirportIds { get; protected set; }
        public string FareIds { get; protected set; }
        public int? MinConnectionMinutes { get; protected set; }
        public int? MaxConnectionMinutes { get; protected set; }

        protected TourFlight() { }

        public TourFlight(
            int groupId,
            int orderIndex,
            string departureCountries,
            string departureCities,
            string arrivalCountries,
            string arrivalCities,
            string airlines,
            string flights,
            string departureAirports,
            string arrivalAirports,
            string fares,
            int? minConnectionMinutes,
            int? maxConnectionMinutes,
            DateTime createdAt
        )
        {
            if (groupId < 0)
                throw new DomainException("Flight group is invalid.");
            TourFlightGroupId = groupId;
            SetValues(
                orderIndex,
                departureCountries,
                departureCities,
                arrivalCountries,
                arrivalCities,
                airlines,
                flights,
                departureAirports,
                arrivalAirports,
                fares,
                minConnectionMinutes,
                maxConnectionMinutes
            );
            SetCreated(createdAt);
        }

        public void Update(
            int orderIndex,
            string departureCountries,
            string departureCities,
            string arrivalCountries,
            string arrivalCities,
            string airlines,
            string flights,
            string departureAirports,
            string arrivalAirports,
            string fares,
            int? minConnectionMinutes,
            int? maxConnectionMinutes,
            DateTime updatedAt
        )
        {
            SetValues(
                orderIndex,
                departureCountries,
                departureCities,
                arrivalCountries,
                arrivalCities,
                airlines,
                flights,
                departureAirports,
                arrivalAirports,
                fares,
                minConnectionMinutes,
                maxConnectionMinutes
            );
            SetUpdated(updatedAt);
        }

        private void SetValues(
            int orderIndex,
            string departureCountries,
            string departureCities,
            string arrivalCountries,
            string arrivalCities,
            string airlines,
            string flights,
            string departureAirports,
            string arrivalAirports,
            string fares,
            int? minConnectionMinutes,
            int? maxConnectionMinutes
        )
        {
            if (orderIndex <= 0)
                throw new DomainException("Order index must be greater than zero.");
            if (minConnectionMinutes < 0 || maxConnectionMinutes < 0)
                throw new DomainException("Connection interval cannot be negative.");
            if (minConnectionMinutes.HasValue != maxConnectionMinutes.HasValue)
                throw new DomainException("Both connection interval limits must be specified.");
            if (minConnectionMinutes > maxConnectionMinutes)
                throw new DomainException("Minimum connection interval cannot exceed maximum.");
            OrderIndex = orderIndex;
            DepartureCountryIds = departureCountries;
            DepartureCityIds = departureCities;
            ArrivalCountryIds = arrivalCountries;
            ArrivalCityIds = arrivalCities;
            AirlineIds = airlines;
            FlightIds = flights;
            DepartureAirportIds = departureAirports;
            ArrivalAirportIds = arrivalAirports;
            FareIds = fares;
            MinConnectionMinutes = minConnectionMinutes;
            MaxConnectionMinutes = maxConnectionMinutes;
        }
    }
}
