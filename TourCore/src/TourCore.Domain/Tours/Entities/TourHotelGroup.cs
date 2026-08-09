using System;
using System.Collections.Generic;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Альтернативный вариант выполнения услуги проживания в составе тура.</summary>
    public class TourHotelGroup : AuditableEntity
    {
        /// <summary>Идентификатор услуги тура, владеющей группой.</summary>
        public int TourServiceId { get; protected set; }

        /// <summary>Услуга тура, владеющая группой.</summary>
        public virtual TourService TourService { get; protected set; }

        /// <summary>Название варианта проживания.</summary>
        public string Name { get; protected set; }

        /// <summary>Приоритет варианта при построении.</summary>
        public int Priority { get; protected set; }

        /// <summary>Позиция группы внутри услуги.</summary>
        public int OrderIndex { get; protected set; }

        /// <summary>Последовательные шаблоны проживания варианта.</summary>
        public virtual ICollection<TourHotel> Hotels { get; protected set; }

        protected TourHotelGroup()
        {
            Hotels = new List<TourHotel>();
        }

        public TourHotelGroup(
            int tourServiceId,
            string name,
            int priority,
            int orderIndex,
            DateTime createdAt
        )
            : this()
        {
            if (tourServiceId <= 0)
                throw new DomainException("Tour service is required.");

            TourServiceId = tourServiceId;
            SetValues(name, priority, orderIndex);
            SetCreated(createdAt);
        }

        private void SetValues(string name, int priority, int orderIndex)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Hotel group name is required.");
            if (name.Trim().Length > 128)
                throw new DomainException("Hotel group name must be 128 characters or less.");
            if (priority <= 0)
                throw new DomainException("Priority must be greater than zero.");
            if (orderIndex <= 0)
                throw new DomainException("Order index must be greater than zero.");

            Name = name.Trim();
            Priority = priority;
            OrderIndex = orderIndex;
        }
    }
}
