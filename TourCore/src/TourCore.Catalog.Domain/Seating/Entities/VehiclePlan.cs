using System;
using System.Collections.Generic;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Domain.Seating.Entities
{
    public class VehiclePlan : AuditableEntity
    {
        public int TransportId { get; protected set; }
        public virtual Transport Transport { get; protected set; }

        public int RowsCount { get; protected set; }
        public int ColumnsCount { get; protected set; }
        public int AreaNumber { get; protected set; }

        public string Name { get; protected set; }

        public bool PlanOrientation { get; protected set; }
        public bool IsAircraft { get; protected set; }

        public string Dates { get; protected set; }
        public string Comment { get; protected set; }

        public virtual ICollection<SeatingCell> Cells { get; protected set; }

        protected VehiclePlan()
        {
            Cells = new List<SeatingCell>();
        }

        public VehiclePlan(
            int transportId,
            int rowsCount,
            int columnsCount,
            int areaNumber,
            DateTime createdAt,
            string name = null,
            bool planOrientation = false,
            bool isAircraft = false,
            string dates = null,
            string comment = null)
        {
            Cells = new List<SeatingCell>();

            SetTransportId(transportId);
            SetRowsCount(rowsCount);
            SetColumnsCount(columnsCount);
            SetAreaNumber(areaNumber);
            SetName(name);
            SetPlanOrientation(planOrientation);
            SetIsAircraft(isAircraft);
            SetDates(dates);
            SetComment(comment);

            SetCreated(createdAt);
        }

        public void Update(
            int transportId,
            int rowsCount,
            int columnsCount,
            int areaNumber,
            DateTime updatedAt,
            string name = null,
            bool planOrientation = false,
            bool isAircraft = false,
            string dates = null,
            string comment = null)
        {
            SetTransportId(transportId);
            SetRowsCount(rowsCount);
            SetColumnsCount(columnsCount);
            SetAreaNumber(areaNumber);
            SetName(name);
            SetPlanOrientation(planOrientation);
            SetIsAircraft(isAircraft);
            SetDates(dates);
            SetComment(comment);

            SetUpdated(updatedAt);
        }

        private void SetTransportId(int value)
        {
            if (value <= 0)
                throw new DomainException("Vehicle plan transport id must be greater than zero.");

            TransportId = value;
        }

        private void SetRowsCount(int value)
        {
            if (value <= 0)
                throw new DomainException("Vehicle plan rows count must be greater than zero.");

            RowsCount = value;
        }

        private void SetColumnsCount(int value)
        {
            if (value <= 0)
                throw new DomainException("Vehicle plan columns count must be greater than zero.");

            ColumnsCount = value;
        }

        private void SetAreaNumber(int value)
        {
            if (value <= 0)
                throw new DomainException("Vehicle plan area number must be greater than zero.");

            AreaNumber = value;
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Name = null;
                return;
            }

            value = value.Trim();

            if (value.Length > 20)
                throw new DomainException("Vehicle plan name must be 20 characters or less.");

            Name = value;
        }

        private void SetPlanOrientation(bool value)
        {
            PlanOrientation = value;
        }

        private void SetIsAircraft(bool value)
        {
            IsAircraft = value;
        }

        private void SetDates(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Dates = null;
                return;
            }

            Dates = value.Trim();
        }

        private void SetComment(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Comment = null;
                return;
            }

            value = value.Trim();

            if (value.Length > 250)
                throw new DomainException("Vehicle plan comment must be 250 characters or less.");

            Comment = value;
        }
    }
}