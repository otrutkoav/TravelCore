using System;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Services.Entities;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourService : AuditableEntity
    {
        public int TourId { get; protected set; }
        public virtual Tour Tour { get; protected set; }
        public int ServiceTypeId { get; protected set; }
        public virtual ServiceType ServiceType { get; protected set; }
        public string Name { get; protected set; }
        public int OrderIndex { get; protected set; }
        public int? ParentServiceId { get; protected set; }
        public virtual TourService ParentService { get; protected set; }
        public bool IsHidden { get; protected set; }
        public bool IsRemovable { get; protected set; }
        public bool IsOptional { get; protected set; }
        public long Revision { get; protected set; }
        public virtual System.Collections.Generic.ICollection<TourFlightGroup> FlightGroups
        {
            get;
            protected set;
        }

        protected TourService()
        {
            FlightGroups = new System.Collections.Generic.List<TourFlightGroup>();
        }

        public TourService(
            int tourId,
            int serviceTypeId,
            string name,
            int orderIndex,
            int? parentServiceId,
            bool isHidden,
            bool isRemovable,
            bool isOptional,
            DateTime createdAt
        )
        {
            if (tourId <= 0)
                throw new DomainException("Tour is required.");
            if (serviceTypeId <= 0)
                throw new DomainException("Service type is required.");
            TourId = tourId;
            ServiceTypeId = serviceTypeId;
            SetName(name);
            SetOrderIndex(orderIndex);
            ParentServiceId = parentServiceId;
            SetBehavior(isHidden, isRemovable, isOptional);
            Revision = 1;
            SetCreated(createdAt);
        }

        public void Update(
            string name,
            int serviceTypeId,
            int? parentServiceId,
            bool isHidden,
            bool isRemovable,
            bool isOptional,
            DateTime updatedAt
        )
        {
            if (serviceTypeId <= 0)
                throw new DomainException("Service type is required.");
            SetName(name);
            ServiceTypeId = serviceTypeId;
            ParentServiceId = parentServiceId;
            SetBehavior(isHidden, isRemovable, isOptional);
            checked
            {
                Revision++;
            }
            SetUpdated(updatedAt);
        }

        public void MoveTo(int orderIndex, DateTime updatedAt)
        {
            SetOrderIndex(orderIndex);
            SetUpdated(updatedAt);
        }

        public void IncrementRevision(DateTime updatedAt)
        {
            checked
            {
                Revision++;
            }
            SetUpdated(updatedAt);
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Tour service name is required.");
            value = value.Trim();
            if (value.Length > 256)
                throw new DomainException("Tour service name must be 256 characters or less.");
            Name = value;
        }

        private void SetOrderIndex(int value)
        {
            if (value <= 0)
                throw new DomainException("Order index must be greater than zero.");
            OrderIndex = value;
        }

        private void SetBehavior(bool isHidden, bool isRemovable, bool isOptional)
        {
            if (isHidden && isRemovable)
                throw new DomainException("A hidden tour service cannot be removable.");
            if (isHidden && isOptional)
                throw new DomainException("A hidden tour service cannot be optional.");
            IsHidden = isHidden;
            IsRemovable = isRemovable;
            IsOptional = isOptional;
        }
    }
}
