using System;

namespace TourCore.Shared.Primitives
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        protected AuditableEntity()
        {
        }

        protected AuditableEntity(int id)
            : base(id)
        {
        }

        protected void SetCreated(DateTime createdAt)
        {
            if (createdAt == default(DateTime))
                CreatedAt = DateTime.UtcNow;
            else
                CreatedAt = createdAt;
        }

        protected void SetUpdated(DateTime updatedAt)
        {
            UpdatedAt = updatedAt == default(DateTime)
                ? DateTime.UtcNow
                : updatedAt;
        }

        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}