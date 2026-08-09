using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>
    /// Один последовательный перелёт внутри варианта авиаперевозки тура.
    /// </summary>
    /// <remarks>
    /// Направления, рейсы, авиакомпании, аэропорты и тарифы задаются коллекцией
    /// <see cref="Restrictions"/>. Это позволяет хранить несколько допустимых значений
    /// каждого вида без строковых списков и без связей с внешними справочниками.
    /// </remarks>
    public class TourFlight : AuditableEntity
    {
        public int TourFlightGroupId { get; protected set; }
        public virtual TourFlightGroup TourFlightGroup { get; protected set; }
        public int OrderIndex { get; protected set; }
        public int? MinConnectionMinutes { get; protected set; }
        public int? MaxConnectionMinutes { get; protected set; }
        /// <summary>Допустимые справочные значения для данного перелёта.</summary>
        public virtual ICollection<TourFlightRestriction> Restrictions { get; protected set; }

        protected TourFlight()
        {
            Restrictions = new List<TourFlightRestriction>();
        }

        public TourFlight(
            int groupId,
            int orderIndex,
            int? minConnectionMinutes,
            int? maxConnectionMinutes,
            DateTime createdAt
        )
            : this()
        {
            if (groupId < 0)
                throw new DomainException("Flight group is invalid.");
            TourFlightGroupId = groupId;
            SetValues(orderIndex, minConnectionMinutes, maxConnectionMinutes);
            SetCreated(createdAt);
        }

        public void Update(
            int orderIndex,
            int? minConnectionMinutes,
            int? maxConnectionMinutes,
            DateTime updatedAt
        )
        {
            SetValues(orderIndex, minConnectionMinutes, maxConnectionMinutes);
            SetUpdated(updatedAt);
        }

        private void SetValues(int orderIndex, int? minConnectionMinutes, int? maxConnectionMinutes)
        {
            if (orderIndex <= 0)
                throw new DomainException("Order index must be greater than zero.");
            if (minConnectionMinutes < 0 || maxConnectionMinutes < 0)
                throw new DomainException("Connection interval cannot be negative.");
            if (minConnectionMinutes.HasValue != maxConnectionMinutes.HasValue)
                throw new DomainException("Both connection interval limits must be specified.");
            if (minConnectionMinutes > maxConnectionMinutes)
                throw new DomainException("Minimum connection interval cannot exceed maximum.");
            OrderIndex = orderIndex;
            MinConnectionMinutes = minConnectionMinutes;
            MaxConnectionMinutes = maxConnectionMinutes;
        }

        public void AddRestrictions(TourFlightRestrictionType type, IEnumerable<int> referenceIds)
        {
            foreach (var referenceId in (referenceIds ?? Enumerable.Empty<int>()).Distinct())
                Restrictions.Add(new TourFlightRestriction(type, referenceId));
        }
    }
}
