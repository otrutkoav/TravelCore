using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.SPOTypes;

public sealed class SPOType : AuditableEntity
{
    public const int MaximumNameLength = 20;
    public const int MaximumUserIdLength = 200;

    public string Name { get; private set; } = null!;
    public string NameLat { get; private set; } = null!;
    public string CreatedByUserId { get; private set; } = null!;

    private SPOType()
    {
    }

    public SPOType(string name, string nameLat, string createdByUserId, DateTime createdAt)
    {
        SetNames(name, nameLat);
        CreatedByUserId = NormalizeRequiredName(createdByUserId, nameof(CreatedByUserId), MaximumUserIdLength);
        SetCreated(createdAt);
    }

    public void Update(string name, string nameLat, DateTime updatedAt)
    {
        SetNames(name, nameLat);
        SetUpdated(updatedAt);
    }

    private void SetNames(string name, string nameLat)
    {
        Name = NormalizeRequiredName(name, nameof(Name), MaximumNameLength);
        NameLat = NormalizeRequiredName(nameLat, nameof(NameLat), MaximumNameLength);
    }

    private static string NormalizeRequiredName(string value, string propertyName, int maximumLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException($"{propertyName} is required.");

        value = value.Trim();
        if (value.Length > maximumLength)
            throw new DomainException($"{propertyName} must be {maximumLength} characters or less.");

        return value;
    }
}
