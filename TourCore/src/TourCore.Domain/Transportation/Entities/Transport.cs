using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Transportation.Entities
{
    /// <summary>
    /// Транспорт.
    /// Справочник видов транспорта с указанием вместимости.
    /// </summary>
    public class Transport : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }

        /// <summary>
        /// Количество мест / вместимость.
        /// </summary>
        public short? SeatsCount { get; protected set; }

        /// <summary>
        /// Порядок отображения.
        /// </summary>
        public int ShowOrder { get; protected set; }

        protected Transport()
        {
        }

        public Transport(
            string name,
            DateTime createdAt,
            string nameEn = null,
            short? seatsCount = null,
            int showOrder = 0)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetSeatsCount(seatsCount);
            SetShowOrder(showOrder);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string nameEn = null,
            short? seatsCount = null,
            int showOrder = 0)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetSeatsCount(seatsCount);
            SetShowOrder(showOrder);

            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Transport name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Transport name must be 100 characters or less.");

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
                throw new DomainException("Transport alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetSeatsCount(short? seatsCount)
        {
            if (seatsCount.HasValue && seatsCount.Value < 0)
                throw new DomainException("Transport seats count cannot be negative.");

            SeatsCount = seatsCount;
        }

        private void SetShowOrder(int showOrder)
        {
            if (showOrder < 0)
                throw new DomainException("Transport show order cannot be negative.");

            ShowOrder = showOrder;
        }
    }
}