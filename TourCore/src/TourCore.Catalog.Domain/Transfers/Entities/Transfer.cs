using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Transfers.Entities
{
    /// <summary>
    /// Справочник варианта трансфера.
    /// Описывает маршрут, длительность и параметры отображения.
    /// </summary>
    public class Transfer : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }

        public DateTime? TimeFrom { get; protected set; }
        public DateTime? TimeTo { get; protected set; }

        public string DurationText { get; protected set; }
        public string PlaceFrom { get; protected set; }
        public string PlaceTo { get; protected set; }

        public bool IsMain { get; protected set; }

        public int? CityId { get; protected set; }
        public virtual City City { get; protected set; }

        public int? DirectionId { get; protected set; }
        public virtual TransferDirection Direction { get; protected set; }

        public string Url { get; protected set; }
        public int ShowOrder { get; protected set; }

        public bool AutoApplyFrom { get; protected set; }
        public bool AutoApplyTo { get; protected set; }

        protected Transfer()
        {
        }

        public Transfer(
            string name,
            DateTime createdAt,
            string nameEn = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            string durationText = null,
            string placeFrom = null,
            string placeTo = null,
            bool isMain = false,
            int? cityId = null,
            int? directionId = null,
            string url = null,
            int showOrder = 0,
            bool autoApplyFrom = false,
            bool autoApplyTo = false)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDurationText(durationText);
            SetPlaceFrom(placeFrom);
            SetPlaceTo(placeTo);
            SetIsMain(isMain);
            SetCityId(cityId);
            SetDirectionId(directionId);
            SetUrl(url);
            SetShowOrder(showOrder);
            SetAutoApplyFrom(autoApplyFrom);
            SetAutoApplyTo(autoApplyTo);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string nameEn = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            string durationText = null,
            string placeFrom = null,
            string placeTo = null,
            bool isMain = false,
            int? cityId = null,
            int? directionId = null,
            string url = null,
            int showOrder = 0,
            bool autoApplyFrom = false,
            bool autoApplyTo = false)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDurationText(durationText);
            SetPlaceFrom(placeFrom);
            SetPlaceTo(placeTo);
            SetIsMain(isMain);
            SetCityId(cityId);
            SetDirectionId(directionId);
            SetUrl(url);
            SetShowOrder(showOrder);
            SetAutoApplyFrom(autoApplyFrom);
            SetAutoApplyTo(autoApplyTo);

            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Transfer name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Transfer name must be 100 characters or less.");

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
                throw new DomainException("Transfer alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetTimeFrom(DateTime? timeFrom)
        {
            TimeFrom = timeFrom;
        }

        private void SetTimeTo(DateTime? timeTo)
        {
            TimeTo = timeTo;
        }

        private void SetDurationText(string durationText)
        {
            if (string.IsNullOrWhiteSpace(durationText))
            {
                DurationText = null;
                return;
            }

            durationText = durationText.Trim();

            if (durationText.Length > 5)
                throw new DomainException("Transfer duration text must be 5 characters or less.");

            DurationText = durationText;
        }

        private void SetPlaceFrom(string placeFrom)
        {
            if (string.IsNullOrWhiteSpace(placeFrom))
            {
                PlaceFrom = null;
                return;
            }

            placeFrom = placeFrom.Trim();

            if (placeFrom.Length > 300)
                throw new DomainException("Transfer departure place must be 300 characters or less.");

            PlaceFrom = placeFrom;
        }

        private void SetPlaceTo(string placeTo)
        {
            if (string.IsNullOrWhiteSpace(placeTo))
            {
                PlaceTo = null;
                return;
            }

            placeTo = placeTo.Trim();

            if (placeTo.Length > 300)
                throw new DomainException("Transfer arrival place must be 300 characters or less.");

            PlaceTo = placeTo;
        }

        private void SetIsMain(bool isMain)
        {
            IsMain = isMain;
        }

        private void SetCityId(int? cityId)
        {
            if (cityId.HasValue && cityId.Value <= 0)
                throw new DomainException("Transfer city id must be greater than zero.");

            CityId = cityId;
        }

        private void SetDirectionId(int? directionId)
        {
            if (directionId.HasValue && directionId.Value <= 0)
                throw new DomainException("Transfer direction id must be greater than zero.");

            DirectionId = directionId;
        }

        private void SetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                Url = null;
                return;
            }

            url = url.Trim();

            if (url.Length > 192)
                throw new DomainException("Transfer URL must be 192 characters or less.");

            Url = url;
        }

        private void SetShowOrder(int showOrder)
        {
            if (showOrder < 0)
                throw new DomainException("Transfer show order cannot be negative.");

            ShowOrder = showOrder;
        }

        private void SetAutoApplyFrom(bool autoApplyFrom)
        {
            AutoApplyFrom = autoApplyFrom;
        }

        private void SetAutoApplyTo(bool autoApplyTo)
        {
            AutoApplyTo = autoApplyTo;
        }
    }
}