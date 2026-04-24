using TourCore.Domain.Common.Enums;

namespace TourCore.Domain.Common.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        public EntityStatus Status { get; protected set; } = EntityStatus.Active;

        public bool IsActive => Status == EntityStatus.Active;

        public void Activate()
        {
            Status = EntityStatus.Active;
        }

        public void Deactivate()
        {
            Status = EntityStatus.Inactive;
        }
    }
}