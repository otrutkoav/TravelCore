using System;

namespace TourCore.Domain.Common.Base
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected void SetCreated(DateTime createdAt)
        {
            CreatedAt = createdAt;
            UpdatedAt = createdAt;
        }

        protected void SetUpdated(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}
