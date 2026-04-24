using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Hotels.ValueObjects;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Тип размещения.
    /// Например: SGL, DBL, TRPL, EXB и т.п.
    /// Содержит базовые данные и правила размещения на основных/дополнительных местах.
    /// </summary>
    public class AccommodationType : AuditableEntity
    {
        /// <summary>
        /// Код типа размещения.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Основное название типа размещения.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Альтернативное / латинское название типа размещения.
        /// </summary>
        public string NameEn { get; protected set; }

        /// <summary>
        /// Признак, что это основной тип размещения.
        /// </summary>
        public bool IsMain { get; protected set; }

        /// <summary>
        /// Общий возраст "от", если для типа размещения задано ограничение.
        /// </summary>
        public short? AgeFrom { get; protected set; }

        /// <summary>
        /// Общий возраст "до", если для типа размещения задано ограничение.
        /// </summary>
        public short? AgeTo { get; protected set; }

        /// <summary>
        /// Количество человек на номер / размещение.
        /// </summary>
        public short? PerRoom { get; protected set; }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        public int SortOrder { get; protected set; }

        /// <summary>
        /// Описание типа размещения.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Правило для основных мест.
        /// </summary>
        public AccommodationPlacementRule MainPlacementRule { get; protected set; }

        /// <summary>
        /// Правило для дополнительных мест.
        /// </summary>
        public AccommodationPlacementRule ExtraPlacementRule { get; protected set; }

        protected AccommodationType()
        {
        }

        public AccommodationType(
            string name,
            DateTime createdAt,
            string code = null,
            string nameEn = null,
            bool isMain = false,
            short? ageFrom = null,
            short? ageTo = null,
            short? perRoom = null,
            int sortOrder = 0,
            string description = null,
            AccommodationPlacementRule mainPlacementRule = null,
            AccommodationPlacementRule extraPlacementRule = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetIsMain(isMain);
            SetAgeRange(ageFrom, ageTo);
            SetPerRoom(perRoom);
            SetSortOrder(sortOrder);
            SetDescription(description);
            SetMainPlacementRule(mainPlacementRule);
            SetExtraPlacementRule(extraPlacementRule);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string code = null,
            string nameEn = null,
            bool isMain = false,
            short? ageFrom = null,
            short? ageTo = null,
            short? perRoom = null,
            int sortOrder = 0,
            string description = null,
            AccommodationPlacementRule mainPlacementRule = null,
            AccommodationPlacementRule extraPlacementRule = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetIsMain(isMain);
            SetAgeRange(ageFrom, ageTo);
            SetPerRoom(perRoom);
            SetSortOrder(sortOrder);
            SetDescription(description);
            SetMainPlacementRule(mainPlacementRule);
            SetExtraPlacementRule(extraPlacementRule);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Meal type code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 50)
                throw new DomainException("Accommodation type code must be 50 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Accommodation type name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Accommodation type name must be 100 characters or less.");

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
                throw new DomainException("Accommodation type alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetIsMain(bool isMain)
        {
            IsMain = isMain;
        }

        private void SetAgeRange(short? ageFrom, short? ageTo)
        {
            if (ageFrom.HasValue && ageFrom.Value < 0)
                throw new DomainException("Accommodation type age from cannot be negative.");

            if (ageTo.HasValue && ageTo.Value < 0)
                throw new DomainException("Accommodation type age to cannot be negative.");

            if (ageFrom.HasValue && ageTo.HasValue && ageFrom.Value > ageTo.Value)
                throw new DomainException("Accommodation type age from cannot be greater than age to.");

            AgeFrom = ageFrom;
            AgeTo = ageTo;
        }

        private void SetPerRoom(short? perRoom)
        {
            if (perRoom.HasValue && perRoom.Value < 0)
                throw new DomainException("Accommodation type per-room value cannot be negative.");

            PerRoom = perRoom;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Accommodation type sort order cannot be negative.");

            SortOrder = sortOrder;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Description = null;
                return;
            }

            Description = description.Trim();
        }

        private void SetMainPlacementRule(AccommodationPlacementRule mainPlacementRule)
        {
            MainPlacementRule = mainPlacementRule;
        }

        private void SetExtraPlacementRule(AccommodationPlacementRule extraPlacementRule)
        {
            ExtraPlacementRule = extraPlacementRule;
        }
    }
}