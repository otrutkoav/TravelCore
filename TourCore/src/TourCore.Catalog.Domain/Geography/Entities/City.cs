using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Geography.Entities
{
    public class City : AuditableEntity
    {
        public int CountryId { get; protected set; }
        public virtual Country Country { get; protected set; }

        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string Code { get; protected set; }
        public int SortOrder { get; protected set; }
        public bool IsDeparturePoint { get; protected set; }
        public string TimeZone { get; protected set; }
        public string IataCode { get; protected set; }
        public string Coordinates { get; protected set; }

        public int? RegionId { get; protected set; }
        public virtual Region Region { get; protected set; }

        protected City()
        {
        }

        public City(
            int countryId,
            string name,
            string nameEn,
            string code,
            int sortOrder,
            bool isDeparturePoint,
            DateTime createdAt,
            string timeZone = null,
            string iataCode = null,
            string coordinates = null,
            int? regionId = null)
        {
            SetCountryId(countryId);
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetSortOrder(sortOrder);
            SetIsDeparturePoint(isDeparturePoint);
            SetTimeZone(timeZone);
            SetIataCode(iataCode);
            SetCoordinates(coordinates);
            SetRegionId(regionId);

            SetCreated(createdAt);
        }

        public void Update(
            int countryId,
            string name,
            string nameEn,
            string code,
            int sortOrder,
            bool isDeparturePoint,
            DateTime updatedAt,
            string timeZone = null,
            string iataCode = null,
            string coordinates = null,
            int? regionId = null)
        {
            SetCountryId(countryId);
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetSortOrder(sortOrder);
            SetIsDeparturePoint(isDeparturePoint);
            SetTimeZone(timeZone);
            SetIataCode(iataCode);
            SetCoordinates(coordinates);
            SetRegionId(regionId);

            SetUpdated(updatedAt);
        }

        private void SetCountryId(int countryId)
        {
            if (countryId <= 0)
                throw new DomainException("City country id must be greater than zero.");

            CountryId = countryId;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("City name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("City name must be 100 characters or less.");

            Name = name;
        }

        private void SetNameEn(string nameEn)
        {
            if (string.IsNullOrWhiteSpace(nameEn))
            {
                NameEn = null;
                return;
            }

            nameEn = nameEn.Trim();

            if (nameEn.Length > 100)
                throw new DomainException("City alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("City code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 3)
                throw new DomainException("City code must be 3 characters or less.");

            Code = code;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("City sort order cannot be negative.");

            SortOrder = sortOrder;
        }

        private void SetIsDeparturePoint(bool isDeparturePoint)
        {
            IsDeparturePoint = isDeparturePoint;
        }

        private void SetTimeZone(string timeZone)
        {
            if (string.IsNullOrWhiteSpace(timeZone))
            {
                TimeZone = null;
                return;
            }

            timeZone = timeZone.Trim();

            if (timeZone.Length > 150)
                throw new DomainException("City time zone must be 150 characters or less.");

            TimeZone = timeZone;
        }

        private void SetIataCode(string iataCode)
        {
            if (string.IsNullOrWhiteSpace(iataCode))
            {
                IataCode = null;
                return;
            }

            iataCode = iataCode.Trim().ToUpperInvariant();

            if (iataCode.Length > 3)
                throw new DomainException("City IATA code must be 3 characters or less.");

            IataCode = iataCode;
        }

        private void SetCoordinates(string coordinates)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
            {
                Coordinates = null;
                return;
            }

            coordinates = coordinates.Trim();

            if (coordinates.Length > 30)
                throw new DomainException("City coordinates must be 30 characters or less.");

            Coordinates = coordinates;
        }

        private void SetRegionId(int? regionId)
        {
            if (regionId.HasValue && regionId.Value <= 0)
                throw new DomainException("City region id must be greater than zero.");

            RegionId = regionId;
        }
    }
}