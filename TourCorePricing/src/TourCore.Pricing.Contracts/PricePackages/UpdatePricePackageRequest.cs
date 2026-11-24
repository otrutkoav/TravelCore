namespace TourCore.Pricing.Contracts.PricePackages;

/// <summary>Запрос изменения ценового пакета.</summary>
public sealed class UpdatePricePackageRequest
{
    /// <summary>Название пакета.</summary>
    public string Name { get; init; } = null!;

    /// <summary>Идентификатор страны из Catalog.</summary>
    public int CountryId { get; init; }

    /// <summary>Отключен ли пакет.</summary>
    public bool IsDisabled { get; init; }
}
