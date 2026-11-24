namespace TourCore.Pricing.Contracts.PriceBlocks;

/// <summary>Общие изменяемые данные ценового блока.</summary>
public abstract class PriceBlockWriteRequest
{
    public int PricePackageId { get; init; }
    public int SPOTypeId { get; init; }
    public int SeasonId { get; init; }
    public int ServiceTypeId { get; init; }
    public int PartnerId { get; init; }
    public int CurrencyId { get; init; }
    public string Name { get; init; } = null!;
    public string? Code { get; init; }
    public string? Description { get; init; }
    public string? Comment { get; init; }
    public string? OnlineInfoText { get; init; }
    public DateTime? SaleStartsAt { get; init; }
    public DateTime? SaleEndsAt { get; init; }
    public int? DaysBeforeCheckInFrom { get; init; }
    public int? DaysBeforeCheckInTo { get; init; }
    public bool ExcludeFromRecalculation { get; init; }
}

/// <summary>Запрос создания ценового блока.</summary>
public sealed class CreatePriceBlockRequest : PriceBlockWriteRequest;

/// <summary>Запрос изменения ценового блока.</summary>
public sealed class UpdatePriceBlockRequest : PriceBlockWriteRequest;
