using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Seating.Entities
{
    /// <summary>
    /// Ячейка схемы размещения (место, блок, проход и т.д.).
    /// </summary>
    public class SeatingCell : AuditableEntity
    {
        public int VehiclePlanId { get; protected set; }
        public virtual VehiclePlan VehiclePlan { get; protected set; }

        /// <summary>
        /// Отображаемый номер (например: 1A, 12, и т.д.).
        /// </summary>
        public string Number { get; protected set; }

        /// <summary>
        /// Тип ячейки (место, блок, занято и т.д.).
        /// </summary>
        public SeatType? Type { get; protected set; }

        /// <summary>
        /// Количество мест (если применимо).
        /// </summary>
        public short? SeatsCount { get; protected set; }

        /// <summary>
        /// Индекс в плоском массиве (используется для вычисления row/column).
        /// </summary>
        public int Index { get; protected set; }

        /// <summary>
        /// Дополнительные визуальные границы.
        /// </summary>
        public string Border { get; protected set; }

        protected SeatingCell()
        {
        }

        public SeatingCell(
            int vehiclePlanId,
            int index,
            DateTime createdAt,
            string number = null,
            SeatType? type = null,
            short? seatsCount = null,
            string border = null)
        {
            SetVehiclePlanId(vehiclePlanId);
            SetIndex(index);
            SetNumber(number);
            SetType(type);
            SetSeatsCount(seatsCount);
            SetBorder(border);

            SetCreated(createdAt);
        }

        public void Update(
            int vehiclePlanId,
            int index,
            DateTime updatedAt,
            string number = null,
            SeatType? type = null,
            short? seatsCount = null,
            string border = null)
        {
            SetVehiclePlanId(vehiclePlanId);
            SetIndex(index);
            SetNumber(number);
            SetType(type);
            SetSeatsCount(seatsCount);
            SetBorder(border);

            SetUpdated(updatedAt);
        }

        private void SetVehiclePlanId(int value)
        {
            if (value <= 0)
                throw new DomainException("Seating cell vehicle plan id must be greater than zero.");

            VehiclePlanId = value;
        }

        private void SetIndex(int value)
        {
            if (value < 0)
                throw new DomainException("Seating cell index cannot be negative.");

            Index = value;
        }

        private void SetNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Number = null;
                return;
            }

            value = value.Trim();

            if (value.Length > 4)
                throw new DomainException("Seating cell number must be 4 characters or less.");

            Number = value;
        }

        private void SetType(SeatType? value)
        {
            Type = value;
        }

        private void SetSeatsCount(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Seating cell seats count cannot be negative.");

            SeatsCount = value;
        }

        private void SetBorder(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Border = null;
                return;
            }

            value = value.Trim();

            if (value.Length > 4)
                throw new DomainException("Seating cell border must be 4 characters or less.");

            Border = value;
        }
    }
}