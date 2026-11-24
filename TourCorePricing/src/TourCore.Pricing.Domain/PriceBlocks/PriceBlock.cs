using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.PriceBlocks;

public sealed class PriceBlock : AuditableEntity
{
    public const int MaximumNameLength = 254;
    public const int MaximumCodeLength = 254;
    public const int MaximumDescriptionLength = 254;
    public const int MaximumCommentLength = 1024;
    public const int MaximumOnlineInfoTextLength = 1024;
    public const int MaximumUserIdLength = 200;

    public int PricePackageId { get; private set; }
    public int SPOTypeId { get; private set; }
    public int SeasonId { get; private set; }
    public int ServiceTypeId { get; private set; }
    public int PartnerId { get; private set; }
    public int CurrencyId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? Comment { get; private set; }
    public string? OnlineInfoText { get; private set; }
    public DateTime? SaleStartsAt { get; private set; }
    public DateTime? SaleEndsAt { get; private set; }
    public int? DaysBeforeCheckInFrom { get; private set; }
    public int? DaysBeforeCheckInTo { get; private set; }
    public bool ExcludeFromRecalculation { get; private set; }
    public CostOfferState State { get; private set; }
    public DateTime? SentForReviewAt { get; private set; }
    public DateTime? ActivatedForNetAt { get; private set; }
    public DateTime? ActivatedAt { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;
    public string? SentForReviewByUserId { get; private set; }
    public string? ActivatedForNetByUserId { get; private set; }
    public string? ActivatedByUserId { get; private set; }
    public string? DeactivatedByUserId { get; private set; }

    private PriceBlock()
    {
    }

    public PriceBlock(
        int pricePackageId,
        int spoTypeId,
        int seasonId,
        int serviceTypeId,
        int partnerId,
        int currencyId,
        string name,
        string? code,
        string? description,
        string? comment,
        string? onlineInfoText,
        DateTime? saleStartsAt,
        DateTime? saleEndsAt,
        int? daysBeforeCheckInFrom,
        int? daysBeforeCheckInTo,
        bool excludeFromRecalculation,
        string createdByUserId,
        DateTime createdAt)
    {
        SetReferences(pricePackageId, spoTypeId, seasonId, serviceTypeId, partnerId, currencyId);
        SetContent(name, code, description, comment, onlineInfoText);
        SetSaleConditions(saleStartsAt, saleEndsAt, daysBeforeCheckInFrom, daysBeforeCheckInTo);
        CreatedByUserId = NormalizeRequired(createdByUserId, MaximumUserIdLength, nameof(CreatedByUserId));
        ExcludeFromRecalculation = excludeFromRecalculation;
        State = CostOfferState.New;
        SetCreated(createdAt);
    }

    public void Update(
        int pricePackageId,
        int spoTypeId,
        int seasonId,
        int serviceTypeId,
        int partnerId,
        int currencyId,
        string name,
        string? code,
        string? description,
        string? comment,
        string? onlineInfoText,
        DateTime? saleStartsAt,
        DateTime? saleEndsAt,
        int? daysBeforeCheckInFrom,
        int? daysBeforeCheckInTo,
        bool excludeFromRecalculation,
        DateTime updatedAt)
    {
        SetReferences(pricePackageId, spoTypeId, seasonId, serviceTypeId, partnerId, currencyId);
        SetContent(name, code, description, comment, onlineInfoText);
        SetSaleConditions(saleStartsAt, saleEndsAt, daysBeforeCheckInFrom, daysBeforeCheckInTo);
        ExcludeFromRecalculation = excludeFromRecalculation;
        SetUpdated(updatedAt);
    }

    public void ChangeState(CostOfferState state, string userId, DateTime changedAt)
    {
        userId = NormalizeRequired(userId, MaximumUserIdLength, nameof(userId));
        State = state;
        switch (state)
        {
            case CostOfferState.New:
                break;
            case CostOfferState.InChecked:
                SentForReviewAt = changedAt;
                SentForReviewByUserId = userId;
                break;
            case CostOfferState.ActivatedForNetto:
                ActivatedForNetAt = changedAt;
                ActivatedForNetByUserId = userId;
                break;
            case CostOfferState.Activated:
                ActivatedAt = changedAt;
                ActivatedByUserId = userId;
                break;
            case CostOfferState.Deactivated:
                DeactivatedAt = changedAt;
                DeactivatedByUserId = userId;
                break;
            default:
                throw new DomainException("Unknown price block state.");
        }
        SetUpdated(changedAt);
    }

    private void SetReferences(int pricePackageId, int spoTypeId, int seasonId, int serviceTypeId, int partnerId, int currencyId)
    {
        PricePackageId = Positive(pricePackageId, nameof(PricePackageId));
        SPOTypeId = Positive(spoTypeId, nameof(SPOTypeId));
        SeasonId = Positive(seasonId, nameof(SeasonId));
        ServiceTypeId = Positive(serviceTypeId, nameof(ServiceTypeId));
        PartnerId = Positive(partnerId, nameof(PartnerId));
        CurrencyId = Positive(currencyId, nameof(CurrencyId));
    }

    private void SetContent(string name, string? code, string? description, string? comment, string? onlineInfoText)
    {
        Name = NormalizeRequired(name, MaximumNameLength, nameof(Name));
        Code = NormalizeOptional(code, MaximumCodeLength, nameof(Code)) ?? string.Empty;
        Description = NormalizeOptional(description, MaximumDescriptionLength, nameof(Description)) ?? string.Empty;
        Comment = NormalizeOptional(comment, MaximumCommentLength, nameof(Comment));
        OnlineInfoText = NormalizeOptional(onlineInfoText, MaximumOnlineInfoTextLength, nameof(OnlineInfoText));
    }

    private void SetSaleConditions(DateTime? startsAt, DateTime? endsAt, int? daysFrom, int? daysTo)
    {
        if (startsAt.HasValue && endsAt.HasValue && startsAt > endsAt)
            throw new DomainException("Sale start date cannot be later than sale end date.");
        if (daysFrom is < 0 || daysTo is < 0)
            throw new DomainException("Days before check-in cannot be negative.");
        if (daysFrom.HasValue && daysTo.HasValue && daysFrom > daysTo)
            throw new DomainException("Days before check-in from cannot be greater than days before check-in to.");

        SaleStartsAt = startsAt;
        SaleEndsAt = endsAt;
        DaysBeforeCheckInFrom = daysFrom;
        DaysBeforeCheckInTo = daysTo;
    }

    private static int Positive(int value, string propertyName)
    {
        if (value <= 0)
            throw new DomainException($"{propertyName} must be greater than zero.");
        return value;
    }

    private static string NormalizeRequired(string value, int maximumLength, string propertyName)
    {
        return NormalizeOptional(value, maximumLength, propertyName)
            ?? throw new DomainException($"{propertyName} is required.");
    }

    private static string? NormalizeOptional(string? value, int maximumLength, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        value = value.Trim();
        if (value.Length > maximumLength)
            throw new DomainException($"{propertyName} must be {maximumLength} characters or less.");
        return value;
    }
}
