using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Transfers.Entities
{
    /// <summary>
    /// Направление трансфера.
    /// </summary>
    public class TransferDirection : AuditableEntity
    {
        public string Name { get; protected set; }

        protected TransferDirection()
        {
        }

        public TransferDirection(string name, DateTime createdAt)
        {
            SetName(name);
            SetCreated(createdAt);
        }

        public void Update(string name, DateTime updatedAt)
        {
            SetName(name);
            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Transfer direction name is required.");

            name = name.Trim();

            if (name.Length > 128)
                throw new DomainException("Transfer direction name must be 128 characters or less.");

            Name = name;
        }
    }
}