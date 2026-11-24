namespace TourCore.Pricing.Contracts.SPOTypes;

/// <summary>Запрос создания типа SPO.</summary>
public sealed class CreateSPOTypeRequest
{
    /// <summary>Название.</summary>
    public string Name { get; init; } = null!;

    /// <summary>Латинское или альтернативное название.</summary>
    public string NameLat { get; init; } = null!;
}
