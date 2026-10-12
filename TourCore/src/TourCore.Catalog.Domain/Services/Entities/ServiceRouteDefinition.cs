using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Services.Entities
{
    /// <summary>
    /// Описание маршрута услуги.
    /// Используется для привязки конкретной услуги к точкам маршрута:
    /// откуда и куда выполняется услуга.
    /// </summary>
    public class ServiceRouteDefinition : AuditableEntity
    {
        /// <summary>
        /// Идентификатор конкретной услуги.
        /// </summary>
        public int ServiceId { get; protected set; }

        /// <summary>
        /// Точка отправления.
        /// </summary>
        public int FromPointId { get; protected set; }
        public virtual GeoPoint FromPoint { get; protected set; }

        /// <summary>
        /// Точка прибытия.
        /// </summary>
        public int ToPointId { get; protected set; }
        public virtual GeoPoint ToPoint { get; protected set; }

        protected ServiceRouteDefinition()
        {
        }

        public ServiceRouteDefinition(
            int serviceId,
            int fromPointId,
            int toPointId,
            DateTime createdAt)
        {
            SetServiceId(serviceId);
            SetFromPointId(fromPointId);
            SetToPointId(toPointId);

            SetCreated(createdAt);
        }

        public void Update(
            int fromPointId,
            int toPointId,
            DateTime updatedAt)
        {
            SetFromPointId(fromPointId);
            SetToPointId(toPointId);

            SetUpdated(updatedAt);
        }

        private void SetServiceId(int value)
        {
            if (value <= 0)
                throw new DomainException("Service id must be greater than zero.");

            ServiceId = value;
        }

        private void SetFromPointId(int value)
        {
            if (value <= 0)
                throw new DomainException("From point id must be greater than zero.");

            FromPointId = value;
        }

        private void SetToPointId(int value)
        {
            if (value <= 0)
                throw new DomainException("To point id must be greater than zero.");

            ToPointId = value;
        }
    }
}