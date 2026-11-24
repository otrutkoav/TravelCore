using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.Seasons;

public sealed class Season : AuditableEntity
{
    public const int MaximumNameLength = 255;
    public const int MaximumUserIdLength = 200;

    public string Name { get; private set; } = null!;
    public string NameLat { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public bool IsMain { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private Season() { }

    public Season(
        string name,
        string nameLat,
        bool isActive,
        bool isMain,
        string createdByUserId,
        DateTime createdAt)
    {
        SetValues(name, nameLat, isActive, isMain);
        CreatedByUserId = Required(createdByUserId, MaximumUserIdLength, nameof(CreatedByUserId));
        SetCreated(createdAt);
    }

    public void Update(string name, string nameLat, bool isActive, bool isMain, DateTime updatedAt)
    {
        SetValues(name, nameLat, isActive, isMain);
        SetUpdated(updatedAt);
    }

    private void SetValues(string name, string nameLat, bool isActive, bool isMain)
    {
        Name = Required(name, MaximumNameLength, nameof(Name));
        NameLat = Required(nameLat, MaximumNameLength, nameof(NameLat));
        IsActive = isActive;
        IsMain = isMain;
    }

    private static string Required(string value, int maximumLength, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException($"{propertyName} is required.");
        value = value.Trim();
        if (value.Length > maximumLength)
            throw new DomainException($"{propertyName} must be {maximumLength} characters or less.");
        return value;
    }
}
