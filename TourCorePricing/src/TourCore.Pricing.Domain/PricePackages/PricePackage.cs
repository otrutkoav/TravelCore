using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.PricePackages;

public sealed class PricePackage : AuditableEntity
{
    public const int MaximumNameLength = 200;
    public const int MaximumCreatedByUserIdLength = 200;

    public string Name { get; private set; } = null!;
    public int CountryId { get; private set; }
    public bool Deleted { get; private set; }
    public bool IsDisabled { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private PricePackage()
    {
    }

    public PricePackage(
        string name,
        int countryId,
        bool isDisabled,
        string createdByUserId,
        DateTime createdAt)
    {
        SetName(name);
        SetCountry(countryId);
        SetCreatedBy(createdByUserId);
        IsDisabled = isDisabled;
        Deleted = false;
        SetCreated(createdAt);
    }

    public void Update(string name, int countryId, bool isDisabled, DateTime updatedAt)
    {
        EnsureNotDeleted();
        SetName(name);
        SetCountry(countryId);
        IsDisabled = isDisabled;
        SetUpdated(updatedAt);
    }

    public void Delete(DateTime updatedAt)
    {
        EnsureNotDeleted();
        Deleted = true;
        SetUpdated(updatedAt);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Price package name is required.");

        name = name.Trim();
        if (name.Length > MaximumNameLength)
            throw new DomainException($"Price package name must be {MaximumNameLength} characters or less.");

        Name = name;
    }

    private void SetCountry(int countryId)
    {
        if (countryId <= 0)
            throw new DomainException("Country identifier must be greater than zero.");

        CountryId = countryId;
    }

    private void SetCreatedBy(string createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(createdByUserId))
            throw new DomainException("Creator user identifier is required.");

        createdByUserId = createdByUserId.Trim();
        if (createdByUserId.Length > MaximumCreatedByUserIdLength)
            throw new DomainException($"Creator user identifier must be {MaximumCreatedByUserIdLength} characters or less.");

        CreatedByUserId = createdByUserId;
    }

    private void EnsureNotDeleted()
    {
        if (Deleted)
            throw new DomainException("Deleted price package cannot be changed.");
    }
}
