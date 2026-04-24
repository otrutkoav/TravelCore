using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Допустимая комбинация параметров проживания:
    /// тип номера + категория номера + тип размещения.
    /// Используется как справочник вариантов проживания.
    /// </summary>
    public class HotelRoomCombination : AuditableEntity
    {
        /// <summary>
        /// Тип номера.
        /// Примеры: SGL, DBL, TRPL, 2 ADL.
        /// </summary>
        public int RoomTypeId { get; protected set; }
        public virtual RoomType RoomType { get; protected set; }

        /// <summary>
        /// Категория номера.
        /// Примеры: Standard, Deluxe, Suite.
        /// </summary>
        public int RoomCategoryId { get; protected set; }
        public virtual RoomCategory RoomCategory { get; protected set; }

        /// <summary>
        /// Тип размещения.
        /// Примеры: 2 Adults, 1 Adult + 1 Child.
        /// </summary>
        public int AccommodationTypeId { get; protected set; }
        public virtual AccommodationType AccommodationType { get; protected set; }

        /// <summary>
        /// Признак основного варианта.
        /// </summary>
        public bool IsMain { get; protected set; }

        /// <summary>
        /// Возраст "от" для данного варианта, если задан.
        /// </summary>
        public short? AgeFrom { get; protected set; }

        /// <summary>
        /// Возраст "до" для данного варианта, если задан.
        /// </summary>
        public short? AgeTo { get; protected set; }

        protected HotelRoomCombination()
        {
        }

        public HotelRoomCombination(
            int roomTypeId,
            int roomCategoryId,
            int accommodationTypeId,
            DateTime createdAt,
            bool isMain = false,
            short? ageFrom = null,
            short? ageTo = null)
        {
            SetRoomTypeId(roomTypeId);
            SetRoomCategoryId(roomCategoryId);
            SetAccommodationTypeId(accommodationTypeId);
            SetIsMain(isMain);
            SetAgeRange(ageFrom, ageTo);

            SetCreated(createdAt);
        }

        public void Update(
            int roomTypeId,
            int roomCategoryId,
            int accommodationTypeId,
            DateTime updatedAt,
            bool isMain = false,
            short? ageFrom = null,
            short? ageTo = null)
        {
            SetRoomTypeId(roomTypeId);
            SetRoomCategoryId(roomCategoryId);
            SetAccommodationTypeId(accommodationTypeId);
            SetIsMain(isMain);
            SetAgeRange(ageFrom, ageTo);

            SetUpdated(updatedAt);
        }

        private void SetRoomTypeId(int value)
        {
            if (value <= 0)
                throw new DomainException("Hotel room combination room type id must be greater than zero.");

            RoomTypeId = value;
        }

        private void SetRoomCategoryId(int value)
        {
            if (value <= 0)
                throw new DomainException("Hotel room combination room category id must be greater than zero.");

            RoomCategoryId = value;
        }

        private void SetAccommodationTypeId(int value)
        {
            if (value <= 0)
                throw new DomainException("Hotel room combination accommodation type id must be greater than zero.");

            AccommodationTypeId = value;
        }

        private void SetIsMain(bool value)
        {
            IsMain = value;
        }

        private void SetAgeRange(short? ageFrom, short? ageTo)
        {
            if (ageFrom.HasValue && ageFrom.Value < 0)
                throw new DomainException("Hotel room combination age from cannot be negative.");

            if (ageTo.HasValue && ageTo.Value < 0)
                throw new DomainException("Hotel room combination age to cannot be negative.");

            if (ageFrom.HasValue && ageTo.HasValue && ageFrom.Value > ageTo.Value)
                throw new DomainException("Hotel room combination age from cannot be greater than age to.");

            AgeFrom = ageFrom;
            AgeTo = ageTo;
        }
    }
}