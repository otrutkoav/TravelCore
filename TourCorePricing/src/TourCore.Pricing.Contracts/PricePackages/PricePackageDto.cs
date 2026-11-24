namespace TourCore.Pricing.Contracts.PricePackages;

/// <summary>Ценовой пакет.</summary>
public sealed class PricePackageDto
{
    /// <summary>Идентификатор пакета.</summary>
    public int Id { get; init; }

    /// <summary>Название пакета.</summary>
    public string Name { get; init; } = null!;

    /// <summary>Идентификатор страны из Catalog.</summary>
    public int CountryId { get; init; }

    /// <summary>Признак мягкого удаления.</summary>
    public bool Deleted { get; init; }

    /// <summary>Признак отключенного пакета.</summary>
    public bool IsDisabled { get; init; }

    /// <summary>Идентификатор создавшего пользователя.</summary>
    public string CreatedByUserId { get; init; } = null!;

    /// <summary>Дата создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>Дата последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
