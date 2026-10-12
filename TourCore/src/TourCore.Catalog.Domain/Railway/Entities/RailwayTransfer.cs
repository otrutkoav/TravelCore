using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Railway.Entities
{
    /// <summary>
    /// ЖД-переезд между городами.
    /// Справочник направлений железнодорожных переездов.
    /// </summary>
    public class RailwayTransfer : AuditableEntity
    {
        public string Name { get; protected set; }

        public int CountryFromId { get; protected set; }
        public virtual Country CountryFrom { get; protected set; }

        public int CityFromId { get; protected set; }
        public virtual City CityFrom { get; protected set; }

        public int CountryToId { get; protected set; }
        public virtual Country CountryTo { get; protected set; }

        public int CityToId { get; protected set; }
        public virtual City CityTo { get; protected set; }

        protected RailwayTransfer()
        {
        }

        public RailwayTransfer(
            string name,
            int countryFromId,
            int cityFromId,
            int countryToId,
            int cityToId,
            DateTime createdAt)
        {
            SetName(name);
            SetCountryFromId(countryFromId);
            SetCityFromId(cityFromId);
            SetCountryToId(countryToId);
            SetCityToId(cityToId);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            int countryFromId,
            int cityFromId,
            int countryToId,
            int cityToId,
            DateTime updatedAt)
        {
            SetName(name);
            SetCountryFromId(countryFromId);
            SetCityFromId(cityFromId);
            SetCountryToId(countryToId);
            SetCityToId(cityToId);

            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Railway transfer name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Railway transfer name must be 100 characters or less.");

            Name = name;
        }

        private void SetCountryFromId(int value)
        {
            if (value <= 0)
                throw new DomainException("Railway transfer departure country id must be greater than zero.");

            CountryFromId = value;
        }

        private void SetCityFromId(int value)
        {
            if (value <= 0)
                throw new DomainException("Railway transfer departure city id must be greater than zero.");

            CityFromId = value;
        }

        private void SetCountryToId(int value)
        {
            if (value <= 0)
                throw new DomainException("Railway transfer arrival country id must be greater than zero.");

            CountryToId = value;
        }

        private void SetCityToId(int value)
        {
            if (value <= 0)
                throw new DomainException("Railway transfer arrival city id must be greater than zero.");

            CityToId = value;
        }
    }
}