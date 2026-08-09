using System;
using System.Collections.Generic;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourFlightGroup : AuditableEntity
    {
        public int TourServiceId { get; protected set; }
        public virtual TourService TourService { get; protected set; }
        public string Name { get; protected set; }
        public int Priority { get; protected set; }
        public int OrderIndex { get; protected set; }
        public virtual ICollection<TourFlight> Flights { get; protected set; }

        protected TourFlightGroup()
        {
            Flights = new List<TourFlight>();
        }

        public TourFlightGroup(
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

        public void Update(string name, int priority, int orderIndex, DateTime updatedAt)
        {
            SetValues(name, priority, orderIndex);
            SetUpdated(updatedAt);
        }

        private void SetValues(string name, int priority, int orderIndex)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Flight group name is required.");
            if (name.Trim().Length > 128)
                throw new DomainException("Flight group name must be 128 characters or less.");
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
