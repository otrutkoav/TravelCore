using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Geography.Entities
{
    /// <summary>
    /// Класс гео-точек (аэропорт, отель, вокзал и т.д.)
    /// </summary>
    public class GeoPointClass : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }

        protected GeoPointClass() { }

        public GeoPointClass(
            string name,
            DateTime createdAt,
            string nameEn = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string nameEn = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetUpdated(updatedAt);
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Geo point class name is required.");

            value = value.Trim();

            if (value.Length > 255)
                throw new DomainException("Geo point class name must be 255 characters or less.");

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
                throw new DomainException("Geo point class name (EN) must be 255 characters or less.");

            NameEn = value;
        }
    }
}