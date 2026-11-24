namespace TourCore.Pricing.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected void SetCreated(DateTime createdAt)
    {
        CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
    }

    protected void SetUpdated(DateTime updatedAt)
    {
        UpdatedAt = updatedAt == default ? DateTime.UtcNow : updatedAt;
    }
}
