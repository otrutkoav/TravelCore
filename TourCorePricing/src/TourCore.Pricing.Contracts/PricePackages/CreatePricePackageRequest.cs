namespace TourCore.Pricing.Contracts.PricePackages;

/// <summary>Запрос создания ценового пакета.</summary>
public sealed class CreatePricePackageRequest
{
    /// <summary>Название пакета.</summary>
    public string Name { get; init; } = null!;

    /// <summary>Идентификатор страны из Catalog.</summary>
    public int CountryId { get; init; }

    /// <summary>Создать пакет отключенным.</summary>
    public bool IsDisabled { get; init; }
}
