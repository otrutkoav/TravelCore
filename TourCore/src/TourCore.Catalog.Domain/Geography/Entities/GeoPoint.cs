using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Geography.Entities
{
    /// <summary>
    /// Географическая точка (аэропорт, отель, вокзал и т.д.)
    /// </summary>
    public class GeoPoint : AuditableEntity
    {
        public int ClassId { get; protected set; }
        public virtual GeoPointClass Class { get; protected set; }

        public int CountryId { get; protected set; }
        public int CityId { get; protected set; }

        public string Name { get; protected set; }
        public string NameEn { get; protected set; }

        protected GeoPoint() { }

        public GeoPoint(
            int classId,
            int countryId,
            int cityId,
            string name,
            DateTime createdAt,
            string nameEn = null)
        {
            SetClassId(classId);
            SetCountryId(countryId);
            SetCityId(cityId);
            SetName(name);
            SetNameEn(nameEn);

            SetCreated(createdAt);
        }

        public void Update(
            int classId,
            int countryId,
            int cityId,
            string name,
            DateTime updatedAt,
            string nameEn = null)
        {
            SetClassId(classId);
            SetCountryId(countryId);
            SetCityId(cityId);
            SetName(name);
            SetNameEn(nameEn);

            SetUpdated(updatedAt);
        }

        private void SetClassId(int value)
        {
            if (value <= 0)
                throw new DomainException("Geo point class id must be greater than zero.");

            ClassId = value;
        }

        private void SetCountryId(int value)
        {
            if (value <= 0)
                throw new DomainException("Geo point country id must be greater than zero.");

            CountryId = value;
        }

        private void SetCityId(int value)
        {
            if (value <= 0)
                throw new DomainException("Geo point city id must be greater than zero.");

            CityId = value;
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Geo point name is required.");

            value = value.Trim();

            if (value.Length > 255)
                throw new DomainException("Geo point name must be 255 characters or less.");

            Name = value;
        }

        private void SetNameEn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                NameEn = null;
                return;
            }

            value = value.Trim();

            if (value.Length > 255)
                throw new DomainException("Geo point name (EN) must be 255 characters or less.");

            NameEn = value;
        }
    }
}