using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Avia.Entities
{
    /// <summary>
    /// Базовый чартерный рейс / маршрут.
    /// Содержит направление, перевозчика, номер рейса и базовые технические параметры.
    /// </summary>
    public class Charter : AuditableEntity
    {
        public int DepartureCityId { get; protected set; }
        public virtual City DepartureCity { get; protected set; }

        public string DepartureAirportCode { get; protected set; }

        public int ArrivalCityId { get; protected set; }
        public virtual City ArrivalCity { get; protected set; }

        public string ArrivalAirportCode { get; protected set; }

        public string AirlineCode { get; protected set; }
        public string FlightNumber { get; protected set; }
        public string AircraftCode { get; protected set; }
        public string AirClassCode { get; protected set; }

        public short? StopsCount { get; protected set; }
        public string TimeChangesCode { get; protected set; }

        protected Charter()
        {
        }

        public Charter(
            int departureCityId,
            int arrivalCityId,
            string flightNumber,
            DateTime createdAt,
            string departureAirportCode = null,
            string arrivalAirportCode = null,
            string airlineCode = null,
            string aircraftCode = null,
            string airClassCode = null,
            short? stopsCount = null,
            string timeChangesCode = null)
        {
            SetDepartureCityId(departureCityId);
            SetArrivalCityId(arrivalCityId);
            SetDepartureAirportCode(departureAirportCode);
            SetArrivalAirportCode(arrivalAirportCode);
            SetAirlineCode(airlineCode);
            SetFlightNumber(flightNumber);
            SetAircraftCode(aircraftCode);
            SetAirClassCode(airClassCode);
            SetStopsCount(stopsCount);
            SetTimeChangesCode(timeChangesCode);

            SetCreated(createdAt);
        }

        public void Update(
            int departureCityId,
            int arrivalCityId,
            string flightNumber,
            DateTime updatedAt,
            string departureAirportCode = null,
            string arrivalAirportCode = null,
            string airlineCode = null,
            string aircraftCode = null,
            string airClassCode = null,
            short? stopsCount = null,
            string timeChangesCode = null)
        {
            SetDepartureCityId(departureCityId);
            SetArrivalCityId(arrivalCityId);
            SetDepartureAirportCode(departureAirportCode);
            SetArrivalAirportCode(arrivalAirportCode);
            SetAirlineCode(airlineCode);
            SetFlightNumber(flightNumber);
            SetAircraftCode(aircraftCode);
            SetAirClassCode(airClassCode);
            SetStopsCount(stopsCount);
            SetTimeChangesCode(timeChangesCode);

            SetUpdated(updatedAt);
        }

        private void SetDepartureCityId(int value)
        {
            if (value <= 0)
                throw new DomainException("Charter departure city id must be greater than zero.");

            DepartureCityId = value;
        }

        private void SetArrivalCityId(int value)
        {
            if (value <= 0)
                throw new DomainException("Charter arrival city id must be greater than zero.");

            ArrivalCityId = value;
        }

        private void SetDepartureAirportCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                DepartureAirportCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 4)
                throw new DomainException("Charter departure airport code must be 4 characters or less.");

            DepartureAirportCode = value;
        }

        private void SetArrivalAirportCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ArrivalAirportCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 4)
                throw new DomainException("Charter arrival airport code must be 4 characters or less.");

            ArrivalAirportCode = value;
        }

        private void SetAirlineCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AirlineCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 3)
                throw new DomainException("Charter airline code must be 3 characters or less.");

            AirlineCode = value;
        }

        private void SetFlightNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Charter flight number is required.");

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 4)
                throw new DomainException("Charter flight number must be 4 characters or less.");

            FlightNumber = value;
        }

        private void SetAircraftCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AircraftCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 3)
                throw new DomainException("Charter aircraft code must be 3 characters or less.");

            AircraftCode = value;
        }

        private void SetAirClassCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AirClassCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 10)
                throw new DomainException("Charter air class code must be 10 characters or less.");

            AirClassCode = value;
        }

        private void SetStopsCount(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Charter stops count cannot be negative.");

            StopsCount = value;
        }

        private void SetTimeChangesCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                TimeChangesCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 1)
                throw new DomainException("Charter time changes code must be 1 character or less.");

            TimeChangesCode = value;
        }
    }
}