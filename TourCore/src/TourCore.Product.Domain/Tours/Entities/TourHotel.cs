using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Шаблон одного участка проживания внутри группы услуги тура.</summary>
    public class TourHotel : AuditableEntity
    {
        /// <summary>Идентификатор группы, владеющей шаблоном проживания.</summary>
        public int TourHotelGroupId { get; protected set; }

        /// <summary>Группа, владеющая шаблоном проживания.</summary>
        public virtual TourHotelGroup TourHotelGroup { get; protected set; }

        /// <summary>Позиция последовательного участка проживания внутри группы.</summary>
        public int OrderIndex { get; protected set; }

        /// <summary>Идентификатор страны из внешнего справочника без внешнего ключа.</summary>
        public int CountryId { get; protected set; }

        /// <summary>Допустимые города проживания.</summary>
        public virtual ICollection<TourHotelCity> Cities { get; protected set; }

        /// <summary>Допустимые продолжительности проживания.</summary>
        public virtual ICollection<TourHotelDuration> Durations { get; protected set; }

        /// <summary>Допустимые отели из внешнего справочника.</summary>
        public virtual ICollection<TourHotelAllowedHotel> AllowedHotels { get; protected set; }

        protected TourHotel()
        {
            Durations = new List<TourHotelDuration>();
            AllowedHotels = new List<TourHotelAllowedHotel>();
            Cities = new List<TourHotelCity>();
        }

        public TourHotel(int tourHotelGroupId, int orderIndex, int countryId, DateTime createdAt)
            : this()
        {
            if (tourHotelGroupId < 0)
                throw new DomainException("Hotel group is invalid.");
            if (orderIndex <= 0)
                throw new DomainException("Order index must be greater than zero.");
            if (countryId <= 0)
                throw new DomainException("Country is required.");

            TourHotelGroupId = tourHotelGroupId;
            OrderIndex = orderIndex;
            CountryId = countryId;
            SetCreated(createdAt);
        }

        public void AddCities(IEnumerable<int> cityIds)
        {
            foreach (var cityId in (cityIds ?? Enumerable.Empty<int>()).Distinct())
                Cities.Add(new TourHotelCity(cityId));
        }

        public void AddDurations(IEnumerable<int> nights)
        {
            foreach (var value in (nights ?? Enumerable.Empty<int>()).Distinct())
                Durations.Add(new TourHotelDuration(value));
        }

        public void AddAllowedHotels(IEnumerable<int> hotelIds)
        {
            foreach (var hotelId in (hotelIds ?? Enumerable.Empty<int>()).Distinct())
                AllowedHotels.Add(new TourHotelAllowedHotel(hotelId));
        }
    }
}
