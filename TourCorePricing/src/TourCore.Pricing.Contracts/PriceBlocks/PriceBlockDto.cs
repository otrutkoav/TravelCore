namespace TourCore.Pricing.Contracts.PriceBlocks;

/// <summary>Ценовой блок.</summary>
public sealed class PriceBlockDto
{
    public int Id { get; init; }
    public int PricePackageId { get; init; }
    public int SPOTypeId { get; init; }
    public int SeasonId { get; init; }
    public int ServiceTypeId { get; init; }
    public int PartnerId { get; init; }
    public int CurrencyId { get; init; }
    public string Name { get; init; } = null!;
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string? Comment { get; init; }
    public string? OnlineInfoText { get; init; }
    public DateTime? SaleStartsAt { get; init; }
    public DateTime? SaleEndsAt { get; init; }
    public int? DaysBeforeCheckInFrom { get; init; }
    public int? DaysBeforeCheckInTo { get; init; }
    public bool ExcludeFromRecalculation { get; init; }
    public byte State { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public DateTime? SentForReviewAt { get; init; }
    public DateTime? ActivatedForNetAt { get; init; }
    public DateTime? ActivatedAt { get; init; }
    public DateTime? DeactivatedAt { get; init; }
    public string CreatedByUserId { get; init; } = null!;
    public string? SentForReviewByUserId { get; init; }
    public string? ActivatedForNetByUserId { get; init; }
    public string? ActivatedByUserId { get; init; }
    public string? DeactivatedByUserId { get; init; }
}
